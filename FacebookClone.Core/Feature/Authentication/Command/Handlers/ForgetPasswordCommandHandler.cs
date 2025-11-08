using FacebookClone.Core.Feature.Authentication.Command.Models;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Authentication.Command.Handlers
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationsService _authenticationsService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailService _emailService;
        public ForgetPasswordCommandHandler(UserManager<User> userManager,IAuthenticationsService authenticationsService,
            IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _authenticationsService = authenticationsService;
            _userManager = userManager;
            _contextAccessor = httpContextAccessor;
            _emailService = emailService;
        }
        public async Task<string> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user!=null)
            {
                var token =await  _userManager.GeneratePasswordResetTokenAsync(user);
                var tokenBytes = Encoding.UTF8.GetBytes(token);
                var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);
                var context = _contextAccessor.HttpContext?.Request;
                var link = $"{context.Scheme}://{context.Host}/api/User/ResetPassword?Email={user.Email}&token={encodedToken}";
                await _emailService.SendEmail(user.Email, $"Click here to reset your password: {link}");

                return "Reset password link sent successfully";
            }
            return "the user not found";
        }
    }
}
