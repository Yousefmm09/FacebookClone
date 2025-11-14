using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacebookClone.Service.Dto
{
    public class CommentDto
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}