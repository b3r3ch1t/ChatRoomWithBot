using System.Text;
using ChatRoomWithBot.Domain.Interfaces;

namespace ChatRoomWithBot.Domain;

public class OperationResult<TResult> : IOperationResult<TResult>
{
    public TResult Result { get; set; }
    public bool Error { get; set; }
    public string Message { get; set; }

    public void ExceptionIfFailed()
    {
        if ( Error )
        {
            throw new Exception( Message );
        }
    }

    public OperationResult( string error )
    {
        Error = true;
        Message = error;
    }

    public OperationResult( Exception e )
    {
        Error = true;
        if ( e != null )
        {
            Message = e.Message;
        }
    }

    public OperationResult( TResult result )
    {
        Result = result;
        Error = false;
        Message = string.Empty;
    }

    public OperationResult(List<string> message)
    {
        var sb = new StringBuilder();
        foreach (var m in message)
        {
            sb.AppendLine(m);
        }

        Message = sb.ToString();
    }
}