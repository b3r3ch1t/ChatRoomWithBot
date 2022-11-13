﻿using ChatRoomWithBot.Domain.Bus;

namespace ChatRoomWithBot.Domain.Commands
{
    public  class BotMessageEvent: Event
    {
        public string StockCode { get; protected  set; }

        public BotMessageEvent(Guid userId, string message,string stockCode) : base(userId, message)
        {
            StockCode = stockCode;
        }
    }
}
