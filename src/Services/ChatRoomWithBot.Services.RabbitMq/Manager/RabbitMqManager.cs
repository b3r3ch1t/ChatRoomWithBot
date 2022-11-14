using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Services.RabbitMq.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ChatRoomWithBot.Services.RabbitMq.Manager
{
    internal class RabbitMqManager : IRabbitMqManager
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IBerechitLogger _berechitLogger;


        public RabbitMqManager(IOptions<RabbitMqSettings> rabbitMqSettings, IBerechitLogger berechitLogger)
        {
             
            _rabbitMqSettings = rabbitMqSettings.Value ;
            _berechitLogger = berechitLogger;

            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitMqSettings.Connection.HostName,
                    UserName = _rabbitMqSettings.Connection.Username,
                    Password = _rabbitMqSettings.Connection.Password
                };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
                throw;
            }
        }

        public void Register()
        {
            _channel.QueueDeclare(
                 queue: _rabbitMqSettings.BotResponseQueue.Name,
                 durable: _rabbitMqSettings.BotResponseQueue.Durable,
                 exclusive: _rabbitMqSettings.BotResponseQueue.Exclusive,
                 autoDelete: _rabbitMqSettings.BotResponseQueue.AutoDelete,
             arguments: null);


        }
 
        public void DeRegister()
        {
            _connection.Close();
        }
    }
}
