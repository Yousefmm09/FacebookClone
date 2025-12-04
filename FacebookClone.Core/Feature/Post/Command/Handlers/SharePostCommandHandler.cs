using FacebookClone.Core.Feature.Post.Command.Models;
using FacebookClone.Data.Entities;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Post.Command.Handlers
{
    public class SharePostCommandHandler : IRequestHandler<SharePostCommand, PostShareDto>
    {
        private readonly IPostService _postService;
        public SharePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<PostShareDto> Handle(SharePostCommand request, CancellationToken cancellationToken)
        {
            var res =await  _postService.SharePost(request.postId);
            return res;
        }
    }
}
