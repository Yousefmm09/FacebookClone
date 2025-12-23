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
    public class VerifyOtpEmailHandler: IRequestHandler<VerifyOtpEmailCommand, string>
    {
        private readonly IAuthenticationsService _service;

        public VerifyOtpEmailHandler(IAuthenticationsService  service)
        {
            _service = service;
        }

        public async Task<string> Handle(
            VerifyOtpEmailCommand request,
            CancellationToken cancellationToken)
        {
            return await _service.VerifyOtpAsync(
                request.UserId,
                request.Code
            );
        }
    }

}
