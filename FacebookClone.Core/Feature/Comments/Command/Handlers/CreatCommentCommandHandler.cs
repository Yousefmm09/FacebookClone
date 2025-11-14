using FacebookClone.Core.Feature.Comments.Command.Models;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Comments.Command.Handlers
{
    public class CreatCommentCommandHandler : IRequestHandler<CreatCommentCommand, CommentDto>
    {
        private readonly ICommentService _commentService;
        public CreatCommentCommandHandler(ICommentService comment)
        {
            _commentService= comment;
        }
        public async Task<CommentDto> Handle(CreatCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentService.CreatComment(new CreateCommentDto
            {
                Content = request.Content,
                PostId = request.PostId,
                ParentCommentId = request.ParentCommentId
            });

            return comment;
        }

    }
}
