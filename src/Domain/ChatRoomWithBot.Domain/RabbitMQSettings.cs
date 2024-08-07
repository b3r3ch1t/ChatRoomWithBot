namespace ChatRoomWithBot.Domain
{
	public class RabbitMQSettings
	{
		public string HostName { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string botChatQueue { get; set; }
		public string botCommandQueue { get; set; }
	}
	 
}
