//using FacebookClone.Core.Feature.Comments.Command.Models;
//using FacebookClone.Infrastructure.Abstract;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FacebookClone.Core.Feature.Comments.Command.Handlers
//{
//    public class EditCommentCommandHandler : IRequestHandler<EditCommentCommand, string>
//    {
//        private readonly ICommentRepository _commentRepository;
//        public EditCommentCommandHandler(ICommentRepository commentRepository)
//        {
//            _commentRepository = commentRepository;
//        }
//        public async Task<string> Handle(EditCommentCommand request, CancellationToken cancellationToken)
//        {
//            var comment = await _commentRepository.GetCommentById(request.CommentId);
//            if (comment == null)
//            {
//                throw new Exception("Comment not found");
//            }
//            comment.Content = request.Content;
//            await _commentRepository.EditComment(request.CommentId,comment);
//            return "Comment updated successfully";
//        }
//    }
//}
