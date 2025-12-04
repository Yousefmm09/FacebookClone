using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Admin.Queries
{
    public class GetAllUserQuery:IRequest<List<UserDto>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
