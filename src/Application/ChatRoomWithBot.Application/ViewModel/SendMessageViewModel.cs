namespace ChatRoomWithBot.Application.ViewModel
{
    public abstract  class  SendMessageViewModel
    { 
        public virtual  bool IsBot => false ;
        public string? UserName { get; set; }
    }
}
