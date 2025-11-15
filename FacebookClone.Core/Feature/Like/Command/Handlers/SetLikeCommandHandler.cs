using FacebookClone.Core.Feature.Like.Command.Models;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using MediatR;

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
}
