using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Data.Entities
{
    public class PostMedia
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; } = null!;

        public string Url { get; set; } = null!;
        public string MediaType { get; set; } = "Image"; // Image, Video
    }
}
