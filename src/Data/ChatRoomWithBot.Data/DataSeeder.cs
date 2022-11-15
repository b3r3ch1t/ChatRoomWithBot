using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Data
{
    public class DataSeeder 
    {
        private readonly ChatRoomWithBotContext _context;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IBerechitLogger _berechitLogger; 

        public DataSeeder(IBerechitLogger berechitLogger, ChatRoomWithBotContext context, UserManager<UserIdentity> userManager) 
        {
            _berechitLogger = berechitLogger;
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {

            _context.Database.EnsureCreated();

            SeedUsers();

            SeedChatRooms();
        }

        private void SeedChatRooms()
        {
            try
            {
                if (_context.ChatRooms.Any()) return; 


                var listChatRooms = new List<ChatRoom>()
                {
                    new ChatRoom(name: "Room 1"),
                    new ChatRoom(name: "Room 2"),
                    new ChatRoom(name: "Room 3"),

                }; 

                _context.ChatRooms.AddRange(listChatRooms);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
            }
        }

        private void SeedUsers()
        {
            try
            {
                if (!_context.Users.Any())
                {
                     

                        _context.Database.EnsureCreated();
                        var user = new UserIdentity()
                        {

                            Email = "user1@teste.com",
                            UserName = "user1@teste.com",
                            Name = "Jane Doe",
                            EmailConfirmed = true,

                        };

                        var x = _userManager.CreateAsync(user, "Test12345678").GetAwaiter().GetResult();


                        user = new UserIdentity()
                        {

                            Email = "user2@teste.com",
                            UserName = "user2@teste.com",
                            Name = "John Doe",
                            EmailConfirmed = true,

                        };

                        _userManager.CreateAsync(user, "Test12345678").GetAwaiter().GetResult();

                    
                }
            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
                throw;
            }
        }
    }
}
