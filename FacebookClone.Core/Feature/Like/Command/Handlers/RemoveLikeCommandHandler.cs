//using FacebookClone.Core.Feature.Like.Command.Models;
//using FacebookClone.Service.Abstract;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FacebookClone.Core.Feature.Like.Command.Handlers
//{
//    public class RemoveLikeCommandHandler : IRequestHandler<RemoveLikeCommand, string>
//    {
//        private readonly ILikeSerivce _likeService;

//        public RemoveLikeCommandHandler(ILikeSerivce likeService)
//        {
//            _likeService = likeService;
//        }

//        public async Task<string> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
//        {
//            return await _likeService.RemoveLike(request.PostId);
//        }
//    }
//}
