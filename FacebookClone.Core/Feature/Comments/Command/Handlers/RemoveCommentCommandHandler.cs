//using FacebookClone.Core.Feature.Comments.Command.Models;
//using FacebookClone.Service.Abstract;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FacebookClone.Core.Feature.Comments.Command.Handlers
//{
//    public class RemoveCommentCommandHandler : IRequestHandler<RemoveCommentCommand, string>
//    {
//        private readonly ICommentService _commentService;

//        public RemoveCommentCommandHandler(ICommentService commentService)
//        {
//            _commentService = commentService;
//        }

//        public async Task<string> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
//        {
//            return await _commentService.RemoveComment(request.CommentId);
//        }
//    }
//}
