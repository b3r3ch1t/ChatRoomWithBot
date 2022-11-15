namespace ChatRoomWithBot.Services.RabbitMq.Settings
{
    public class RabbitMqSettings
    {
        public Connection Connection { get; set; }
        public string BotChatQueue { get; set; }
        public string BotCommandQueue { get; set; }
    }
}
