namespace ChatRoomWithBot.Domain.Interfaces;

public interface IEntity
{
    Guid Id { get;  }
    bool Valid { get; }
    DateTime DateCreated { get; }
    void ChangeDateCreated(DateTime date);
    void Activate();
    void Deactivate();
    void ChangeId();
}