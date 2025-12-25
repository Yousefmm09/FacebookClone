using FacebookClone.Core.Feature.Post.Command.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Users.Command.Validation
{
    public class CreatePost : AbstractValidator<CreatPostCommand>
    {
        public CreatePost()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Post content must not be empty")
                .MaximumLength(500).WithMessage("Post content must not exceed 500 characters");
            RuleFor(x => x.Privacy).IsInEnum().WithMessage("Invalid privacy setting");
            RuleFor(x => x.ParentPostId).GreaterThanOrEqualTo(0).WithMessage("Parent Post Id must be greater than or equal to 0");
        }
        //custom
        public void ApplyCustomValidation()
        {

        }
    }
}
