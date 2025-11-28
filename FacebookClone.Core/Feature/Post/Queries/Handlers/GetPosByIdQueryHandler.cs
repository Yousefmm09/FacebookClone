using FacebookClone.Core.Feature.Post.Queries.Models;
using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Post.Queries.Handlers
{
    public class GetPosByIdQueryHandler : IRequestHandler<GetPosByIdQuery, PostDto>
    {
        private readonly IPostService _postService; 
        public GetPosByIdQueryHandler(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<PostDto> Handle(GetPosByIdQuery request, CancellationToken cancellationToken)
        {
            var getPost =  await _postService.GetPostById(request.PostId);
            return new PostDto
            {
                Content = getPost.Content,
                PostId = getPost.PostId,
                Privacy=getPost.Privacy,
                LikeCount=getPost.LikeCount,
            };
        }
    }
}
