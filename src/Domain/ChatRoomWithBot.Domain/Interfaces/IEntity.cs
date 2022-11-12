namespace ChatRoomWithBot.Domain.Interfaces;

public interface IEntity
{
    Guid Id { get; set; }
    bool Valid { get; }
    DateTime DateModification { get; }
    DateTime DateAdded { get; }


    void ChangeDateModification(DateTime date);
    void ChangeDateAdded(DateTime date);
    void Activate();

    void Deactivate();
    void ChangeId();
}