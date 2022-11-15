using Bogus;
using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatRoomWithBot.Data.Test
{
    internal static class ChatRoomWithBotContextFaker
    {
        public const int MinChatMessages = 200;
        public const int MaxChatMessages = 500;


        public static ChatRoomWithBotContext GetChatRoomWithBotContextInMemory()
        {
            var builder = new DbContextOptionsBuilder<ChatRoomWithBotContext>();
            builder.UseInMemoryDatabase(databaseName: "ChatRoomWithBotContext");

            var dbContextOptions = builder.Options;
            var dbContext = new ChatRoomWithBotContext(dbContextOptions);
            // Delete existing db before creating a new one
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var qteMessages = Randomizer.Seed.Next(minValue: MinChatMessages, maxValue: MaxChatMessages );

            var listChatMessage = new List<ChatMessage>();
            var roomId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var faker = new Faker();
            var userName = faker.Person.FullName;

            for (var i = 0; i < qteMessages; i++)
            {
                faker = new Faker();
                var message = faker.Lorem.Sentence();

                var c = new ChatMessage(userId, message, userName, roomId);

                listChatMessage.Add(c);
            } 

            dbContext.ChatMessages.AddRange(listChatMessage);
            dbContext.SaveChanges();

            return dbContext; 
        }

       
    }
}
