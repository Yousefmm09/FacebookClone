using FacebookClone.Core.Feature.Post.Command.Models;
using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Service.Abstract;
using MediatR;

namespace FacebookClone.Core.Feature.Post.Command.Handlers
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
