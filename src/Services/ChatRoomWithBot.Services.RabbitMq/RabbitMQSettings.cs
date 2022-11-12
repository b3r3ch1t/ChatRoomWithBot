using ChatRoomWithBot.Services.RabbitMq.Settings;

namespace ChatRoomWithBot.Services.RabbitMq
{
    public class RabbitMqSettings
    {
        public Connection Connection { get; set; }
        public Queue BotBundleQueue { get; set; }
        public Queue BotResponseQueue { get; set; } 
    }
}
