using FacebookClone.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Queries.Models
{
    public class GetFriendsModel:IRequest<List<Friendship>>
    {
        public string userId {  get; set; }
    }
}
