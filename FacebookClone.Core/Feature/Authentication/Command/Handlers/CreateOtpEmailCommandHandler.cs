using FacebookClone.Core.Feature.Authentication.Command.Models;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Authentication.Command.Handlers
{
    public class CreateOtpEmailHandler: IRequestHandler<CreateOtpEmailCommand, string>
    {
        private readonly IAuthenticationsService _service;

        public CreateOtpEmailHandler(IAuthenticationsService service)
        {
            _service = service;
        }

        public async Task<string> Handle(
            CreateOtpEmailCommand request,
            CancellationToken cancellationToken)
        {
            return await _service.CreateOtpAsync(
                request.UserId,
                request.Email
            );
        }
    }
}
