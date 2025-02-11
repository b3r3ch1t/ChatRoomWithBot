namespace ChatRoomWithBot.Domain.OperationResults;

public class OperationResultIEnumerable<T> : OperationResult<T>
{
    public int TotalRecords { get; set; }
}