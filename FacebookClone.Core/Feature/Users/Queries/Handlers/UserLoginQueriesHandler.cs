using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Core.Feature.Users.Queries.Models;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Queries.Handlers
{
    public class UserLoginQueriesHandler : IRequestHandler<UserLoginModel, JwtToken>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationsService _authenticationsService;
        private readonly SignInManager<User> _signInManager;
        public UserLoginQueriesHandler(UserManager<User> userManager,IAuthenticationsService authenticationsService,SignInManager<User> signInManager)
        {
            _authenticationsService = authenticationsService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<JwtToken> Handle(UserLoginModel request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new JwtToken
                {
                    Message = "Please register first before logging in."
                };
            }

            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!checkPassword.Succeeded)
            {
                return new JwtToken
                {
                    Message = "Invalid password. Please try again."
                };
            }

            
            var accessToken = await _authenticationsService.CreateAccessTokenAsync(user);

            return new JwtToken
            {
                Message = "Login successful",
                AccessToken = accessToken.AccessToken,
                refreshToken = accessToken.refreshToken
            };
        }

        // creat refresh Token here 
    }
}
