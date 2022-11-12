using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ChatRoomWithBot.Data.IdentityModel
{
    public class UserIdentity:IdentityUser<Guid>, IEntity
    {
        public Guid Id { get; protected set; }

        public string Name { get; set; }
         
        public DateTime DateModification { get; set; }
        public DateTime DateCreated { get; set; }
        public IEnumerable<ChatMessage> ChatMessages { get; set; }

        public void ChangeDateModification(DateTime date)
        {
            DateModification = date;
        }

        public void ChangeDateCreated(DateTime date)
        {
            DateCreated = date;
        }
        
        public void ChangeId()
        {
            Id = Guid.NewGuid();
        }
    }
}
