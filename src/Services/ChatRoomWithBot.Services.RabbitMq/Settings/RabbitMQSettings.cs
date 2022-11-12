﻿namespace ChatRoomWithBot.Services.RabbitMq.Settings
{
    public class RabbitMqSettings
    {
        public Connection Connection { get; set; }
        public Queue BotBundleQueue { get; set; }
        public Queue BotResponseQueue { get; set; }
    }
}