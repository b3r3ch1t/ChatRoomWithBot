using System.Text;
using System.Text.Json;
using ChatRoomWithBot.Service.WorkerService.Events;
using ChatRoomWithBot.Service.WorkerService.Settings; 
using Microsoft.Extensions.Options;
using Quartz;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatRoomWithBot.Service.WorkerService
{
    internal class ConsumerRabbitMq : IJob
    {
        private readonly RabbitMqSettings _rabbitMqSettings;


        public ConsumerRabbitMq(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Serilog.Log.Information($"Worker TimerJob  --> {DateTime.Now}");
            
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitMqSettings.Connection.HostName,
                    UserName = _rabbitMqSettings.Connection.Username,
                    Password = _rabbitMqSettings.Connection.Password
                };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();


                channel.QueueDeclare(
                    queue: _rabbitMqSettings.BotBundleQueue.Name,
                    durable: _rabbitMqSettings.BotBundleQueue.Durable,
                    exclusive: _rabbitMqSettings.BotBundleQueue.Exclusive,
                    autoDelete: _rabbitMqSettings.BotBundleQueue.AutoDelete,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray() ;
                        var message = Encoding.UTF8.GetString(body);
                        var order =  JsonSerializer.Deserialize<ChatBotMessage>(message);
                        Console.WriteLine(" [x] Received {0}", message);

                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        //Logger
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }


                    channel.BasicConsume(queue: _rabbitMqSettings.BotBundleQueue.Name,
                        autoAck: false,
                        consumer: consumer);

                };
                 


            }
            catch (Exception e)
            {
                Serilog.Log.Error(e.Message);

            }

            return Task.FromResult(true);
        }
    }
}
