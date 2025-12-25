using FacebookClone.Core.Feature.Authentication.Command.Models;
using FacebookClone.Service.Abstract;
using MediatR;

namespace FacebookClone.Core.Feature.Authentication.Command.Handlers
{
    public class VerifyOtpEmailHandler : IRequestHandler<VerifyOtpEmailCommand, string>
    {
        private readonly IAuthenticationsService _service;

        public VerifyOtpEmailHandler(IAuthenticationsService service)
        {
            _service = service;
        }

        public async Task<string> Handle(
            VerifyOtpEmailCommand request,
            CancellationToken cancellationToken)
        {
            return await _service.VerifyOtpAsync(
                request.UserName,
                request.Code
            );
        }
    }

}
