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
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdModel, CommentDto>
    {
        private readonly ICommentService _commentService;

        public GetCommentByIdQueryHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<CommentDto> Handle(GetCommentByIdModel request, CancellationToken cancellationToken)
        {
            return await _commentService.GetCommentById(request.Id);
        }
    }
}
