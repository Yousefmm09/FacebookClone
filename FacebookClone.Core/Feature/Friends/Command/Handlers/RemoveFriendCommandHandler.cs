using FacebookClone.Core.Feature.Friends.Command.Models;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Friends.Command.Handlers
{
    public class RemoveFriendCommandHandler : IRequestHandler<RemoveFriendCommand, string>
    {
        private readonly IFriendService _friendService;
        public RemoveFriendCommandHandler(IFriendService friendService)
        {
            _friendService = friendService;
        }
        public Task<string> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
        {
            var rmFriend = _friendService.RemoveFriendShip(request.FriendId);
            return rmFriend;
        }
    }
}
