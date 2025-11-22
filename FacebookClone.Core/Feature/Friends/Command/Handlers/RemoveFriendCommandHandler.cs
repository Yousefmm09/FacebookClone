using FacebookClone.Core.Feature.Friends.Command.Models;
using FacebookClone.Service.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Friends.Command.Handlers
{
    public class RemoveFriendCommandHandler : IRequestHandler<RemoveFriendCommand, string>
    {
        private readonly IFriendService _friendService;
        private readonly IHttpContextAccessor _contextAccessor;

        public RemoveFriendCommandHandler(IFriendService friendService, IHttpContextAccessor contextAccessor)
        {
            _friendService = friendService;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");

            await _friendService.RemoveFriendShip(userId, request.FriendId);

            return "Friend removed successfully";
        }

    }

}
