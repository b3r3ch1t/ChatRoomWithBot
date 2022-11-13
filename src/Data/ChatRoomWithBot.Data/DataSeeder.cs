using System.Runtime.CompilerServices;
using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Data
{
    public class DataSeeder : ClassBase
    {
        private readonly ChatRoomWithBotContext _context;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IError _error; 

        public DataSeeder(IError error, ChatRoomWithBotContext context, UserManager<UserIdentity> userManager) : base(error)
        {
            _error = error;
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {

            SeedUsers();

            SeedChatRooms();


            

        }

        private void SeedChatRooms()
        {
            try
            {
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
                _error.Error(e);
            }
        }

        private void SeedUsers()
        {
            try
            {
                if (!_context.Users.Any())
                {
                    ExecuteSafe(() =>
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

                    });
                }
            }
            catch (Exception e)
            {
                _error.Error(e);
                throw;
            }
        }
    }
}
