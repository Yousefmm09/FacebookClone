using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Authentication.Queries.Models
{
    public class ConfirmEmailQuery:IRequest<string>
    {
        public string userId { get; set; }=string.Empty;
        public string token { get; set; }=string.Empty ;
    }
}
