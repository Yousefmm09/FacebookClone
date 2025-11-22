using FacebookClone.Core.Feature.Posts.Command.Models;
using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Posts.Command.Handlers
{
    public class CreatPostCommandHandler : IRequestHandler<CreatPostCommand, PostDto>
    {
        private readonly IPostService _postService;
        public CreatPostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }
        public Task<PostDto> Handle(CreatPostCommand request, CancellationToken cancellationToken)
        {
            var post = _postService.CreatPostAsync(new PostDto
            {
                Content = request.Content,
                ParentPostId = request.ParentPostId,
                Privacy = request.Privacy,
            });
            return post;
        }
    }
}
