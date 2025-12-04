using FacebookClone.Data.Entities.Identity;
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
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly IAdminService _adminService;

        public GetAllUserHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.GetAllUser(request.PageSize, request.PageNumber);
        }
    }
}
