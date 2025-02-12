

using Bogus;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.Services;
using ChatRoomWithBot.Domain.Validators;
using Moq;

namespace ChatRoomWithBot.Domain.Test
{
    public class ChatManagerDomainTest
    {

        private Mock<IMediatorHandler> _mediatorHandlerMock;
        private Mock<EventValidator> _validatorMock;
        private Mock<IBerechitLogger> _berechitLoggerMock;

        public ChatManagerDomainTest()
        {
            _mediatorHandlerMock = new Mock<IMediatorHandler>();
            _validatorMock = new Mock<EventValidator>();
            _berechitLoggerMock = new Mock<IBerechitLogger>();
        }

        [Fact]
        public async Task Validade_MessageIsNotValid()
        {
            var faker = new Faker();

            var messageBeforValidation = "/test";
            var codeRoom = Guid.NewGuid();
            var userName = faker.Person.FullName;
            var userId = Guid.NewGuid();


            var chatMessageCommandEvent = new ChatMessageCommandEvent
            {
                Message = messageBeforValidation,
                CodeRoom = codeRoom,
                UserName = userName,
                UserId = userId
            };

            _mediatorHandlerMock.Setup(x => x.SendMessage(It.IsAny<Event>()))
                .ReturnsAsync(CommandResponse.Ok);


            var chatManagerDomain =
                new ChatManagerDomain(mediatorHandler: _mediatorHandlerMock.Object,
                    validator: _validatorMock.Object,
                    berechitLogger: _berechitLoggerMock.Object

                );


            var result = await chatManagerDomain.SendMessageAsync(chatMessageCommandEvent);
            var msgAfterValidation = chatMessageCommandEvent.Message;


            Assert.True(result.Success);

            Assert.Equal(messageBeforValidation, msgAfterValidation);

            _mediatorHandlerMock.Verify(x => x
                .SendMessage(It.IsAny<Event>()), Times.Once());

            _berechitLoggerMock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);

        }



        [Fact]
        public async Task Validade_IsCommand()
        {
            var faker = new Faker();

            var messageBeforValidation = $"/stock=aapl.us";
            var codeRoom = Guid.NewGuid();
            var userName = faker.Person.FullName;
            var userId = Guid.NewGuid();


            var chatMessageCommandEvent = new ChatMessageCommandEvent
            {
                Message = messageBeforValidation,
                CodeRoom = codeRoom,
                UserName = userName,
                UserId = userId
            };

            _mediatorHandlerMock.Setup(x => x.SendMessage(It.IsAny<Event>()))
                .ReturnsAsync(CommandResponse.Ok);


            var chatManagerDomain =
                new ChatManagerDomain(mediatorHandler: _mediatorHandlerMock.Object,
                    validator: _validatorMock.Object,
                    berechitLogger: _berechitLoggerMock.Object

                );


            var result = await chatManagerDomain.SendMessageAsync(chatMessageCommandEvent);
            var msgAfterValidation = chatMessageCommandEvent.Message;


            Assert.True(result.Success);

            Assert.True(chatMessageCommandEvent.UserName == "bot");

            Assert.Equal(messageBeforValidation, msgAfterValidation);

            _mediatorHandlerMock.Verify(x => x
                .SendMessage(It.IsAny<Event>()), Times.Once());

            _berechitLoggerMock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);


        }




        [Fact]
        public async Task Validade_Exception()
        {
            var faker = new Faker();

            var messageBeforValidation = $"/stock=aapl.us";
            var codeRoom = Guid.NewGuid();
            var userName = faker.Person.FullName;
            var userId = Guid.NewGuid();


            var chatMessageCommandEvent = new ChatMessageCommandEvent
            {
                Message = messageBeforValidation,
                CodeRoom = codeRoom,
                UserName = userName,
                UserId = userId
            };

            _mediatorHandlerMock.Setup(x => x.SendMessage(It.IsAny<Event>()))
                .Throws(new Exception("erro"));


            var chatManagerDomain =
                new ChatManagerDomain(mediatorHandler: _mediatorHandlerMock.Object,
                    validator: _validatorMock.Object,
                    berechitLogger: _berechitLoggerMock.Object

                );


            var result = await chatManagerDomain.SendMessageAsync(chatMessageCommandEvent);
            var msgAfterValidation = chatMessageCommandEvent.Message;


            Assert.True(result.Failure);



            Assert.True(chatMessageCommandEvent.UserName == "bot");

            Assert.Equal(messageBeforValidation, msgAfterValidation);



            _berechitLoggerMock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);


        }
    }

}
