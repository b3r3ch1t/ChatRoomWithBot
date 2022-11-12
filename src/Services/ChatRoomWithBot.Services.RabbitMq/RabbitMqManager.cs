using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatRoomWithBot.Services.RabbitMq
{
    internal class RabbitMqManager : IRabbitMqManager
    {
        private readonly IConnection _connection;
        private readonly IModel _channel; 
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IError _error;


        public RabbitMqManager(IDependencyResolver dependencyResolver)
        {
            _error = dependencyResolver.Resolve<IError>(); 
            _rabbitMqSettings = dependencyResolver.Resolve<IOptions<RabbitMqSettings>>().Value ;

            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqSettings.Connection.HostName,
                UserName = _rabbitMqSettings.Connection.Username,
                Password = _rabbitMqSettings.Connection.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Register( )
        {
           _channel.QueueDeclare(
                queue: _rabbitMqSettings.BotResponseQueue.Name,
                durable: _rabbitMqSettings.BotResponseQueue.Durable,
                exclusive: _rabbitMqSettings.BotResponseQueue.Exclusive,
                autoDelete: _rabbitMqSettings.BotResponseQueue.AutoDelete,
            arguments: null);

           
        }

        public void SendMessage(string message)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;

                _error.Information(" [x] Received {0}", message);
            };
            _channel.BasicConsume(queue: _rabbitMqSettings.BotResponseQueue.Name, autoAck: true, consumer: consumer);
        }

        public void DeRegister()
        {
            _connection.Close();
        }
    }
}
