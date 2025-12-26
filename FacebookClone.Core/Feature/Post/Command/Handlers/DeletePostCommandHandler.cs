using FacebookClone.Core.Feature.Post.Command.Models;
using FacebookClone.Service.Abstract;
using MediatR;

namespace FacebookClone.Core.Feature.Post.Command.Handlers
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, string>
    {
        private readonly IPostService _postService;
        public DeletePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<string> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var deletePost = await _postService.DeletePost(request.PostId);
            if (deletePost != null)
            {
                return deletePost;
            }

            return "the post not deleted";
        }
    }
}
