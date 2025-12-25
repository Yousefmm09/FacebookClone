using FacebookClone.Core.Feature.Users.Command.Models;
using FluentValidation;

namespace FacebookClone.Core.Feature.Users.Command.Validation
{
    public class UserRegister : AbstractValidator<UserRegisterModel>
    {
        public UserRegister()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Name must not be Empty")
                .NotNull().WithMessage("Name must not be null")
                .MaximumLength(10).WithMessage("Max length is 10");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must not be Empty")
                .NotNull().WithMessage("Email must not be null")
                .EmailAddress().WithMessage("Invalid Email Address");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must not be Empty")
                .NotNull().WithMessage("Password must not be null")
                .MinimumLength(6).WithMessage("Min length is 6");

        }

    }
}
