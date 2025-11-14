using FacebookClone.Data.Entities;
using FacebookClone.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Abstract
{
    public interface ICommentService
    {
        public Task<CommentDto> CreatComment(CreateCommentDto comment);
    }
}
