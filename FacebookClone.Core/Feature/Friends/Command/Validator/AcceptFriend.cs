using FacebookClone.Core.Feature.Friends.Command.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Friends.Command.Validator
{
    public class GetFriendList : AbstractValidator<AcceptFriendCommand>
    {
        public GetFriendList()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required")
               .Must(id => int.TryParse(id, out var value) && value > 0)
               .WithMessage("UserId must be a valid number greater than 0");

        }
    }

}
