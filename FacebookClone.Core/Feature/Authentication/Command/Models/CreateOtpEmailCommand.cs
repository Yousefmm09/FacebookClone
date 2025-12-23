using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Authentication.Command.Models
{
    public record CreateOtpEmailCommand(string UserId, string Email)
      : IRequest<string>;

}
