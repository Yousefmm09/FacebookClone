using MediatR;

namespace FacebookClone.Core.Feature.Authentication.Command.Models
{
    public record VerifyOtpEmailCommand(string UserName, string Code)
     : IRequest<string>;

}
