using FacebookClone.Data.Entities;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Admin.Command.Models
{
    public class UnbannedUserModel:IRequest<MessageDto>
    {
        public string UserId {  get; set; }
    }
}
