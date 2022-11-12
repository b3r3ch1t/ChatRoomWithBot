using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ChatRoomWithBot.Data.IdentityModel
{
    public class User : IdentityUser<Guid>, IEntity
    {

        public string Name { get; set; }

        public override string UserName => Email;
        public bool Valid { get; set; }
        public DateTime DateModification { get; set; }
        public DateTime DateAdded { get; set; }
        public void ChangeDateModification(DateTime date)
        {
            DateModification = date;
        }

        public void ChangeDateAdded(DateTime date)
        {
            DateAdded = date;
        }

        public void Activate()
        {
            Valid = true;
        }

        public void Deactivate()
        {
            Valid = false;
        }

        public void ChangeId()
        {
            Id = Guid.NewGuid();
        }
    }
}
