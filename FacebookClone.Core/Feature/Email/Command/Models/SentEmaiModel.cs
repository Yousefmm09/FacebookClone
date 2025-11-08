using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Email.Command.Models
{
    public class SentEmaiModel:IRequest<string>
    {
        public string Email { get; set; }=string.Empty;
        public string Message {  get; set; }=string.Empty ;
    }
}
