using FacebookClone.Core.Feature.Post.Command.Models;
using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Post.Command.Handlers
{
    public class UpdatePostCommandHandler:IRequestHandler<UpdatePostCommand,string>
    {
        private readonly IPostService _postService;
        public UpdatePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<string> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetPostById(request.PostId);
            var updatePost = await  _postService.UpdatePost(new PostDto
            {
                Content = request.Content,
                Privacy = request.Privacy,
                UpdatedAt = DateTime.UtcNow,
            }, request.PostId);
            if(updatePost != null)
            {
                return $"the post is updated {updatePost}";
            }
            return "the post is not updated";
        }
    }
}
