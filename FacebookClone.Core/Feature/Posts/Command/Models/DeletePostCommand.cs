using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Posts.Command.Models
{
    public class DeletePostCommand:IRequest<string>
    {
        public int PostId { get; set; } = 0;
    }
}
