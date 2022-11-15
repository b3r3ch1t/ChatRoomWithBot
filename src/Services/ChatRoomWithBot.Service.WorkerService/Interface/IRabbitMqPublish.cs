using ChatRoomWithBot.Domain.Events;

namespace ChatRoomWithBot.Service.WorkerService.Interface;

public interface IRabbitMqPublish
{
    Task SendMessage(string host, string queue, ChatMessageCommandEvent chatMessageCommandEvent);
}