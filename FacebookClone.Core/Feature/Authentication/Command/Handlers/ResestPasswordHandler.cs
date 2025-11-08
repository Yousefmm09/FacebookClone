using FacebookClone.Core.Feature.Authentication.Command.Models;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Authentication.Command.Handlers
{
    public class ResestPasswordHandler : IRequestHandler<ResestPasswordCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationsService _authenticationsService;
        public ResestPasswordHandler(UserManager<User> userManager,IAuthenticationsService authenticationsService)
        {
            _userManager = userManager;
            _authenticationsService = authenticationsService;
        }

        public async Task<string> Handle(ResestPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return "User not found";

            if (request.NewPassword != request.ConfirmPassword)
                return "Password and confirmation do not match";

            var decodedBytes = WebEncoders.Base64UrlDecode(request.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedBytes);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);
            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            return "Password has been reset successfully";
        }
    }
}
