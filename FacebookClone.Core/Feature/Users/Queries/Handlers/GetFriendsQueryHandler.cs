using FacebookClone.Core.Feature.Users.Queries.Models;
using FacebookClone.Data.Entities;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Queries.Handlers
{
    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsModel, List<Friendship>>
    {
        private readonly IUserService _user;
        public GetFriendsQueryHandler(IUserService user)
        {
            _user=user;
        }
        public async Task<List<Friendship>>Handle(GetFriendsModel request, CancellationToken cancellationToken)
        {
            var friends = await _user.GetFriends(request.userId);
            return friends;
        }
    }
}
