using FacebookClone.Core.Feature.Admin.Command.Models;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Admin.Command.Handlers
{
    public class BannedUserHandler : IRequestHandler<BannedUserModel, MessageDto>
    {
        private readonly IAdminService _adminService;
        public BannedUserHandler(IAdminService adminService) 
        {
            _adminService = adminService;
        }
        public async Task<MessageDto> Handle(BannedUserModel request, CancellationToken cancellationToken)
        {
            var banned= await _adminService.BannedUser(request.UserId,request.BannedReason);
            return banned;
        }
    }
}
