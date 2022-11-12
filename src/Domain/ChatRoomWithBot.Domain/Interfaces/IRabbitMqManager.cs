namespace ChatRoomWithBot.Domain.Interfaces;

public  interface IRabbitMqManager
{
    void Register();
    void SendMessage(string message);
    void DeRegister();


}