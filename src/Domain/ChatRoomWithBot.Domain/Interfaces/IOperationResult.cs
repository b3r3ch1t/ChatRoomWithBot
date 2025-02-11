namespace ChatRoomWithBot.Domain.Interfaces
{

	public interface IOperationResult<T>
	{
		T Data { get; set; }
		public bool Error => !Success;

		public bool Success { get; internal set; }

		public string Message { get;   }

	}
}