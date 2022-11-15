using ChatRoomWithBot.Service.WorkerService.Interface;
using ChatRoomWithBot.Domain.Events;
using MassTransit;

namespace ChatRoomWithBot.Service.WorkerService.Services
{
    internal class RabbitMqPublish: IRabbitMqPublish
    {
        private readonly IBus _bus;

        public RabbitMqPublish(IBus bus)
        {
            _bus = bus;
        }
        public async  Task SendMessage(string host, string queue, ChatMessageCommandEvent chatMessageCommandEvent)
        {
            try
            {
                var uri = new Uri($"rabbitmq://{host}/{queue}");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(chatMessageCommandEvent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
