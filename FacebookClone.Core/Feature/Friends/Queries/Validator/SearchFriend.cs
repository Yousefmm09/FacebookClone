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
                 .NotEmpty().WithMessage("UserId is required");
        }
    }
}
