using FacebookClone.Core.Feature.Friends.Command.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Friends.Command.Validator
{
    public class SentFriend : AbstractValidator<SentFriendRequestCommand>
    {
        public SentFriend()
        {
            RuleFor(x => x.ReceiverId)
                .NotEmpty().WithMessage("ReceiverId is required")
               .Must(id => int.TryParse(id, out var value) && value > 0)
               .WithMessage("ReceiverId must be a valid number greater than 0");
        }
    }
}
