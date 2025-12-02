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
    public class UnbannedUserHandler : IRequestHandler<UnbannedUserModel, MessageDto>
    {
        private readonly IAdminService _adminService;
        public UnbannedUserHandler(IAdminService adminService) 
        {
            _adminService = adminService;
        }
        public async Task<MessageDto> Handle(UnbannedUserModel request, CancellationToken cancellationToken)
        {
            var banned= await _adminService.UnbannedUser(request.UserId);
            return banned;
        }
    }
}
