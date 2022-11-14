using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ChatRoomWithBot.Domain.Validators
{
    public class EventValidator : AbstractValidator<Event>
    {
        private readonly IChatRoomRepository _chatRoomRepository;
        public EventValidator(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;


            ValidateCodeRoom();
            ValidateMessage();
            ValidateCommand();
        }

        private void ValidateCommand()
        {
            When(x => x.IsBotCommand , () =>
            {
                RuleFor(x => x.Message)
                    .MustAsync(CommandIsValid)
                    .WithMessage("The command is not valid ");
            });

        }

        private Task<bool> CommandIsValid(string command, CancellationToken cancellationToken)
        {
            return Task.FromResult(command.StartsWith("/stock="));
        }

        private void ValidateMessage()
        {
            RuleFor(x => x.Message)
                .NotEmpty().NotNull().WithMessage(" the message is Invalid");
        }

         
         

        private void ValidateCodeRoom()
        {
            RuleFor(x => x.CodeRoom)
                .MustAsync(ExistsRoomIdAsync)
                .WithMessage(" This room is invalid !");
        }

        private async Task<bool> ExistsRoomIdAsync(Guid roomId, CancellationToken cancellationToken)
        {
            var result = await _chatRoomRepository.ExistsRoomIdAsync(roomId);

            return result;

        }
    }
}
