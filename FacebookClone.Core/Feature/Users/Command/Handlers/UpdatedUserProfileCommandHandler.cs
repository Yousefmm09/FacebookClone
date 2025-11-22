using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Command.Handlers
{
    public class UpdatedUserProfileCommandHandler : IRequestHandler<UpdatedUserProfileModel, string>
    {
        private readonly IUserService _userService;
        public UpdatedUserProfileCommandHandler(IUserService userService)
        {
         _userService = userService;   
        }

        public Task<string> Handle(UpdatedUserProfileModel request, CancellationToken cancellationToken)
        {
            var update = _userService.UpdateProfile(new User
            {
                UserName=request.Name, 
                Email=request.Email,
                PhoneNumber=request.PhoneNumber,
                ProfilePictureUrl=request.ProfilePic
            });
            return update;
        }
    }
}
