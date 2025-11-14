using FacebookClone.Data.Entities;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public CommentService(ICommentRepository commentRepository,IHttpContextAccessor httpContextAccessor
            ,IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _contextAccessor = httpContextAccessor;
            _postRepository = postRepository;
        }
        public async Task<CommentDto> CreatComment(CreateCommentDto commentDto)
        {
            var userId=_contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");
            var post=  await _postRepository.GetPostById(commentDto.PostId);
            if(post == null)
                throw new Exception("Not found post");
            var comment = new Comment()
            {
                UserId= userId,
                Content=commentDto.Content,
                ParentCommentId=commentDto.ParentCommentId,
                CreatedAt= DateTime.Now,
                PostId= commentDto.PostId,
                //Replies= commentDto.Replies,
            };
            post.CommentCount += 1;
           await _commentRepository.CreatComment(comment);
            return new CommentDto
            {
                Content=comment.Content,
               CreatedAt=comment.CreatedAt,
               PostId= comment.PostId,
               //Replies= comment.Replies,
            };
        }
    }
}
