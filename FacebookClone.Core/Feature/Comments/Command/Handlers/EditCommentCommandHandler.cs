using FacebookClone.Core.Feature.Comments.Command.Models;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Comments.Command.Handlers
{
    public class EditCommentCommandHandler : IRequestHandler<EditCommentCommand, string>
    {
        private readonly ICommentService commentService;
        public EditCommentCommandHandler(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        public async Task<string> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await commentService.GetCommentById(request.CommentId);
            if (comment == null)
            {
                throw new Exception("Comment not found");
            }
            comment.Content = request.Content;
             var res= await commentService.EditComment(request.CommentId,comment);
                return res;
        }
    }
}
