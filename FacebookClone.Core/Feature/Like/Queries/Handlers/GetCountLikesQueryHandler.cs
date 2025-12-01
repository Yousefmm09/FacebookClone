using FacebookClone.Core.Feature.Like.Queries.Models;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Like.Queries.Handlers
{
    public class GetCountLikesQueryHandler:IRequestHandler<GetCountLikesQuery,int>  
    {
        private readonly ILikeSerivce _likeSerivce;
        public GetCountLikesQueryHandler(ILikeSerivce likeSerivce)
        {
            _likeSerivce = likeSerivce;
        }
        public async Task<int> Handle(GetCountLikesQuery request, CancellationToken cancellationToken)
        {
            return await _likeSerivce.GetLikesCount(request.PostId);
        }
    }
}
