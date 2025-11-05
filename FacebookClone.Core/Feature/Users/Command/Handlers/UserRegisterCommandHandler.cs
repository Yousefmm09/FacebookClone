using FacebookClone.Core.Feature.Users.Command;
using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Command.Handlers
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterModel, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationsService _authenticationsService;
        public UserRegisterCommandHandler(UserManager<User> userManager, IAuthenticationsService authenticationsService)
        {
            _userManager = userManager;
            _authenticationsService = authenticationsService;
        }
        public async Task<string> Handle(UserRegisterModel request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
                return "The user already exists";

            if (request.Password != request.ConfirmPassword)
                return "The password does not match the confirmation password";

            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Bio = request.Bio,
                CreatedAt = DateTime.UtcNow,
            };

            var res = await _userManager.CreateAsync(newUser, request.Password);

            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                return "The user is registered successfully";
            }

            var errors = string.Join(", ", res.Errors.Select(e => e.Description));
            return $"Failed to create user: {errors}";
        }

    }
}
