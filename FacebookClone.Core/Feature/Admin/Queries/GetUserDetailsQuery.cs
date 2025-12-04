using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Admin.Queries
{
    public class GetUserDetailsQuery:IRequest<UserDto>
    {
        public string UserId { get; set; }
    }
}
