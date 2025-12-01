using FacebookClone.Core.Feature.Comments.Queries.Models;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Comments.Queries.Handlers
{
    public class GetPostCommentByIdQueryHandler:IRequestHandler<GetPostCommentsModel, IEnumerable<CommentDto>>
    {
        private readonly ICommentService _commentService;
        public GetPostCommentByIdQueryHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public Task<IEnumerable<CommentDto>> Handle(GetPostCommentsModel request, CancellationToken cancellationToken)
        {
            return _commentService.GetPostComments(request.PostId);
        }
    }
}
