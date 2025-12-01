using FacebookClone.Core.Feature.Like.Command.Models;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using MediatR;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Like.Command.Handlers
{
    public class SetLikeCommandHandler : IRequestHandler<SetLikeCommand, string>
    {
        private readonly ILikeSerivce _likeSerivce;
        public SetLikeCommandHandler(ILikeSerivce likeSerivce)
        {
            _likeSerivce = likeSerivce;
        }

        public async Task<string> Handle(SetLikeCommand request, CancellationToken cancellationToken)
        {
            var res = await _likeSerivce.SetLike(new LikeDto
            {
                postId = request.postId,
            });
            return res;
        }
=======

public class SetLikeCommandHandler : IRequestHandler<SetLikeCommand, string>
{
    private readonly ILikeSerivce _likeSerivce;

    public SetLikeCommandHandler(ILikeSerivce likeSerivce)
    {
        _likeSerivce = likeSerivce;
    }

    public async Task<string> Handle(SetLikeCommand request, CancellationToken cancellationToken)
    {
        var res = await _likeSerivce.SetLike(new LikeDto
        {
            postId = request.postId,
        });

        return res;
>>>>>>> Comment
    }
}
