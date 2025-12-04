using FacebookClone.Core.Feature.Friends.Command.Models;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Friends.Command.Handlers
{
    public class AcceptFriendCommandHandler : IRequestHandler<AcceptFriendCommand, FriendshipDto>
    {
        private readonly IFriendService _friendService;
        public AcceptFriendCommandHandler(IFriendService friendService)
        {
            _friendService = friendService;
        }
        public async Task<FriendshipDto> Handle(AcceptFriendCommand request, CancellationToken cancellationToken)
        {
           return await _friendService.AcceptFriend(new FriendshipDto
            {
                UserId= request.UserId,
                CreatedAt= DateTime.UtcNow,
            });
        }
    }
}
