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
    public class SentFriendRequestCommandHandler : IRequestHandler<SentFriendRequestCommand, FriendRequestDto>
    {
        private readonly IFriendService _friendService;
        public SentFriendRequestCommandHandler(IFriendService friendService)
        {
            _friendService = friendService;
        }
        public async Task<FriendRequestDto> Handle(SentFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var res = await _friendService.SendFriendRequest(new FriendRequestDto
            {
                ReceiverId= request.ReceiverId,
                Status=FriendRequestDto.FriendRequestStatus.Pending,
                SentAt= DateTime.UtcNow,
            });
            return res;
        }
    }
}
