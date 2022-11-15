using ChatRoomWithBot.Domain.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWithBot.Service.WorkerService
{
    internal class ChatMessageCommandEventConsumer : IConsumer<ChatMessageCommandEvent>
    {
        public Task Consume(ConsumeContext<ChatMessageCommandEvent> context)
        {
            Console.WriteLine(context.Message );

            return Task.CompletedTask;
        }
    }
}
