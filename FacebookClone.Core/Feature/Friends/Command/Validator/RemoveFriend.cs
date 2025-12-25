using FacebookClone.Core.Feature.Friends.Command.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Friends.Command.Validator
{
    public class GetMyFriendQuery : AbstractValidator<RemoveFriendCommand>
    {
        public GetMyFriendQuery()
        {
            RuleFor(x => x.FriendId)
                .NotEmpty().WithMessage("FriendId is required")
                .Must(id => int.TryParse(id, out var value) && value > 0)
                .WithMessage("FriendId must be a valid number greater than 0");
        }
    }
}
