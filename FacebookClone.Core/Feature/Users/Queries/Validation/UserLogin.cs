using FacebookClone.Core.Feature.Users.Queries.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Users.Command.Validation
{
    public class UserLogin : AbstractValidator<UserLoginModel>
    {
        public UserLogin()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email adress").NotEmpty().WithMessage("The email is empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("The password is empty");
        }
        //custom
        public void ApplyCustomValidation()
        {

        }
    }
}
