using FacebookClone.Core.Feature.Post.Queries.Models;
using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Infrastructure.Context;
using FacebookClone.Service.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Post.Queries.Handlers
{
    public class GetPostsPagedHandler
     : IRequestHandler<GetPostsPagedQuery, PagedResultDto<PostDto>>
    {
        private readonly AppDb _context;

        public GetPostsPagedHandler(AppDb context)
        {
            _context = context;
        }

        public async Task<PagedResultDto<PostDto>> Handle(GetPostsPagedQuery request, CancellationToken ct)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;

            var totalCount = await _context.Posts.CountAsync(ct);

            var items = await _context.Posts
                .AsNoTracking()
                .Include(p => p.user)
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(request.PageSize)
                .Select(p => new PostDto
                {
                    PostId = p.Id,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    LikeCount = p.LikeCount,
                    CommentCount = p.CommentCount,
                    ShareCount = p.ShareCount,
                    UserName = p.user.UserName
                })
                .ToListAsync(ct);

            return new PagedResultDto<PostDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber
            };
        }
    }

}
