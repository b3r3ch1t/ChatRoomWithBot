using System.Text;

namespace ChatRoomWithBot.Domain.Bus
{
    public class CommandResponse
    {
        private CommandResponse(bool isSuccess, string errorMessage)
        {
            Success = isSuccess;
            Message = new List<string> { errorMessage };
        }

        public bool Success { get; }
        public bool Failure => !Success;

        public List<string> Message { get; }


        public static CommandResponse Ok()
        {
            return new CommandResponse(true, string.Empty);
        }

        public static CommandResponse Fail(string errorMessage = "")
        {
            return new CommandResponse(false, errorMessage);
        }

        public static CommandResponse Fail(Exception exception)
        {
            return new CommandResponse(false, exception.Message);
        }


        internal static class ResultMessages
        {
            public static readonly string ErrorObjectIsNotProvidedForFailure =
                "You attempted to create a failure result, which must have an error, but a null error object was passed to the constructor.";

            public static readonly string ErrorObjectIsProvidedForSuccess =
                "You attempted to create a success result, which cannot have an error, but a non-null error object was passed to the constructor.";
        }

        public static CommandResponse Fail(List<string> listErrors)
        {

            var sb = new StringBuilder();

            foreach (var e in listErrors)
            {
                sb.AppendLine(e);
            }

            return new CommandResponse(false, sb.ToString());
        }
    }
}
