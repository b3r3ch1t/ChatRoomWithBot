namespace ChatRoomWithBot.Domain.Interfaces;

public interface IEntity
{
    Guid Id { get;  } 
    DateTime DateCreated { get; }
    void ChangeDateCreated(DateTime date);
    void ChangeId();
}