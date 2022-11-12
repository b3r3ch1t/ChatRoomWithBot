using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Domain;

namespace ChatRoomWithBot.Data
{
    public class DataSeeder : ClassBase
    {
        private readonly ChatRoomWithBotContext _context;
        private readonly UserManager<User> _userManager;

        public DataSeeder(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            _context = dependencyResolver.Resolve<ChatRoomWithBotContext>();
            _userManager = dependencyResolver.Resolve<UserManager<User>>();

        }

        public void Seed()
        {
            if (!_context.Users.Any())
            {
                ExecuteSafe(() =>
                {
                    var user = new User()
                    {

                        Email = "user1@teste.com",
                        UserName = "user1@teste.com",
                        Name = "Jane Doe",
                        EmailConfirmed = true,

                    };

                     _userManager.CreateAsync(user).GetAwaiter().GetResult();


                    user = new User()
                    {

                        Email = "user2@teste.com",
                        UserName = "user2@teste.com",
                        Name = "John Doe",
                        EmailConfirmed = true,

                    };

                    _userManager.CreateAsync(user).GetAwaiter().GetResult();

                });
            }

        }
    }
}
