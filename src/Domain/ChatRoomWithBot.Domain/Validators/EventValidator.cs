using ChatRoomWithBot.Domain.Events;
using FluentValidation;

namespace ChatRoomWithBot.Domain.Validators
{
    public class EventValidator : AbstractValidator<Event>
    { 
        
        public EventValidator(  )
        {
             
            ValidateMessage();
            ValidateCommand();
        }

        private void ValidateCommand()
        {
            When(x => x.IsCommand , () =>
            {
                RuleFor(x => x.Message)
                    .Must(CommandIsValid)
                    .WithMessage("The command is not valid ");
            });

        }

        private bool CommandIsValid(string command )
        {
            return command.StartsWith("/stock=");
        }

        private void ValidateMessage()
        {
            RuleFor(x => x.Message)
                .NotEmpty().NotNull().WithMessage(" the message is Invalid");
        }

        

        
    }
}
