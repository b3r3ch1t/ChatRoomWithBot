using ChatRoomWithBot.Domain.Validators;

namespace ChatRoomWithBot.Domain.Events
{
    public  class Event  
    {
        

        public string Message { get; set; }
        public Guid CodeRoom { get; set; }
        

        public bool IsCommand => Message.StartsWith("/");

        public string UserName { get; set; }

        public Guid UserId { get; set; }

        public bool IsValid()
        {
            var validate = new EventValidator().Validate(this);

            return validate.IsValid;

        }

    }
}
