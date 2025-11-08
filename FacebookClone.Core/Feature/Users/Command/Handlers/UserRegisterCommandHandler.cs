using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace FacebookClone.Core.Feature.Users.Command.Handlers
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterModel, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationsService _authenticationsService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailService _emailService;

        public UserRegisterCommandHandler(
            UserManager<User> userManager,
            IAuthenticationsService authenticationsService,
            IHttpContextAccessor contextAccessor,
            IEmailService emailService)
        {
            _userManager = userManager;
            _authenticationsService = authenticationsService;
            _contextAccessor = contextAccessor;
            _emailService = emailService;
        }

        public async Task<string> Handle(UserRegisterModel request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return "The user already exists";

            if (request.Password != request.ConfirmPassword)
                return "The password does not match the confirmation password";

            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Bio = request.Bio,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return $"Failed to create user: {errors}";
            }

            await _userManager.AddToRoleAsync(newUser, "User");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var codeBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(codeBytes);

            var requestContext = _contextAccessor.HttpContext?.Request;
            if (requestContext == null)
                return "Cannot generate confirmation link: HttpContext is null";

            var link = $"{requestContext.Scheme}://{requestContext.Host}/api/User/ConfirmEmail?userId={newUser.Id}&token={encodedToken}";

            await _emailService.SendEmail(newUser.Email, link);

            return "The user is registered successfully";
        }
    }
}
