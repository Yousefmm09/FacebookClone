using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Admin.Queries
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDto>
    {
        private readonly IAdminService _admin;
        public GetUserDetailsQueryHandler(IAdminService adminService)
        {
            _admin = adminService;
        }
        public async Task<UserDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var getdetails= await _admin.GetUserDetails(request.UserId);
            return getdetails;
        }
    }
}
