using ChatRoomWithBot.Domain.Bus;
using MediatR;

namespace ChatRoomWithBot.Domain.Commands
{
    public  class BotMessageEvent: Event, INotification
    {
        public string StockCode { get; protected  set; }

        public BotMessageEvent(Guid userId, string message,string stockCode) : base(userId, message)
        {
            StockCode = stockCode;
        }
    }
}
