using FacebookClone.Core.Feature.Users.Queries.Models;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Queries.Handlers
{
    public class GetProfileUserQueryHandler : IRequestHandler<GetProfileUserModel, ProfileUserDto>
    {
        private readonly IUserService _user;
        public GetProfileUserQueryHandler(IUserService userService)
        {
            _user=userService;
        }
        public async  Task<ProfileUserDto> Handle(GetProfileUserModel request, CancellationToken cancellationToken)
        {
            var data= await _user.GetProfile();
            return data;
        }
    }
}
