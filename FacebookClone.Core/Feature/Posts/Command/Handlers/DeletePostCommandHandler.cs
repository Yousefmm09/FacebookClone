using FacebookClone.Core.Feature.Posts.Command.Models;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Posts.Command.Handlers
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
            var deletePost =  await _postService.DeletePost(request.PostId);
            if (deletePost != null)
            {
                return deletePost;
            }

            return "the post not deleted";
        }
    }
}
