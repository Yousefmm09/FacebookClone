using FacebookClone.Core.Feature.Friends.Queries.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Friends.Queries.Validator
{
    public class SearchFriend : AbstractValidator<SearchFriendsQuery>
    {
        public SearchFriend()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.SearchTerm).NotEmpty().WithMessage("Search text must not be empty");
            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("UserId is required")
                 .Must(id => int.TryParse(id, out var value) && value > 0)
                 .WithMessage("UserId must be a valid number greater than 0");
        }
    }
}
