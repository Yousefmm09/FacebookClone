using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Like.Command.Models
{
    public class SetLikeCommand:IRequest<string>
    {
       
        public int postId { get; set; }
    }
}
