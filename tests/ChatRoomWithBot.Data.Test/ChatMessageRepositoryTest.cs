using Bogus;
using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Data.Repository;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Interfaces;
using Moq;

namespace ChatRoomWithBot.Data.Test
{
    public class ChatMessageRepositoryTest
    {
        private readonly ChatRoomWithBotContext _dbContext;
        private readonly Mock<IBerechitLogger> _iBerechitLoggerMock;
        public ChatMessageRepositoryTest()
        {
            _dbContext = ChatRoomWithBotContextFaker.GetChatRoomWithBotContextInMemory();
            _iBerechitLoggerMock = new Mock<IBerechitLogger>();
        }


          
        [Fact]
        public async Task AddCommitedAsync_Exception()
        {
            var chatMessageRepository = new ChatMessageRepository(_dbContext, _iBerechitLoggerMock.Object);

            var faker = new Faker();
            var roomId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var userName = faker.Person.FullName;
            var message = faker.Lorem.Sentence();
            var chatMessage = new ChatMessage(userId, message, userName, roomId);

            var qteBefore = _dbContext.ChatMessages.Count(); 
            
            var result = await chatMessageRepository.AddCommitedAsync(chatMessage);

            var qteAfter = _dbContext.ChatMessages.Count();


            Assert.True(result.Success );
            _iBerechitLoggerMock.Verify(x=> x.Error( It.IsAny<Exception >()),Times.Never );

            Assert.Equal(qteAfter , qteBefore +1);



        }
    }
}