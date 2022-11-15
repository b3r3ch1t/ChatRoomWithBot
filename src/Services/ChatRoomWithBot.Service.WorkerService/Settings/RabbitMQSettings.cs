namespace ChatRoomWithBot.Service.WorkerService.Settings
{
    public class RabbitMqSettings
    {
        public Connection Connection { get; set; }
        public Queue botCommandQueue { get; set; }
        public Queue BotResponseQueue { get; set; }
    }
}
