namespace ChatRoomWithBot.Domain.Interfaces;

public interface IOperationResult<TResult>
{
    TResult Result { get; set; }
    bool Error { get; set; }
    string Message { get; set; }
}