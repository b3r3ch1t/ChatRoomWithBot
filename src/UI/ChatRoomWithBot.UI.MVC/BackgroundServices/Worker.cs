using System.Text;
using System.Text.Json;
using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces; 
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IModel = RabbitMQ.Client.IModel;

namespace ChatRoomWithBot.UI.MVC.BackgroundServices
{
    public class Worker : BackgroundService
    {

        private IConnection _connection;
        private IModel? _channel;
        private readonly IBerechitLogger _berechitLogger;
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public Worker(  IServiceScopeFactory serviceScopeFactory)
        {


            using var scope = serviceScopeFactory.CreateScope();
            var scopedServiceProvider = scope
                .ServiceProvider;
            var berechitLogger = scopedServiceProvider
                .GetRequiredService<IBerechitLogger>();


            _berechitLogger = berechitLogger;
            _serviceScopeFactory = serviceScopeFactory;

            InitializeRabbitMQ();

        }

        private void  InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = SharedSettings.Current.RabbitMq.Host,
                Port = SharedSettings.Current.RabbitMq.Port,
                UserName = SharedSettings.Current.RabbitMq.Username,
                Password = SharedSettings.Current.RabbitMq.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: SharedSettings.Current.RabbitMq.Queue,
              durable: true,
              exclusive: false,
              autoDelete: false,
              arguments: null);

            _berechitLogger.Information("Conectado ao RabbitMQ e fila declarada.");
        }


        protected override async  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);


            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var chatMessage = JsonSerializer.Deserialize<ChatMessageCommandEvent>(message);

                using var scope = _serviceScopeFactory.CreateScope();
                var scopedServiceProvider = scope
                    .ServiceProvider;
                var processarService = scopedServiceProvider
                    .GetRequiredService<IProcessarChatMessageCommandEvent>();


                var processado =   processarService
                        .ProcessarChatMessageCommandEventAsync(chatMessage).GetAwaiter().GetResult() ;

                if (processado.Success )
                {
                    _berechitLogger.Information("Mensagem processada com sucesso => Cobrança Expirada");
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                else
                {
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                    _berechitLogger.Error($"Mensagem não  processada com sucesso - {SharedSettings.Current.RabbitMq.Queue}"); 


                }
            };

            _channel.BasicConsume(queue: SharedSettings.Current.RabbitMq.Queue, autoAck: false, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}
