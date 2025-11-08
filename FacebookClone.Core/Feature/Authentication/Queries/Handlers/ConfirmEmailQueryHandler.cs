using FacebookClone.Core.Feature.Authentication.Queries.Models;
using FacebookClone.Service.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Authentication.Queries.Handlers
{
    public class ConfirmEmailQueryHandler:IRequestHandler<ConfirmEmailQuery,string>
    {
        private readonly IAuthenticationsService _authenticationsService;
        public ConfirmEmailQueryHandler(IAuthenticationsService authenticationsService)
        {
            _authenticationsService = authenticationsService;
        }

        public async Task<string> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var codeBytes = WebEncoders.Base64UrlDecode(request.token);
            var decodedCode = Encoding.UTF8.GetString(codeBytes);
            var res=  await _authenticationsService.ConfirmEmail(request.userId, decodedCode);
            if (res.Message== "the email is confirmed")
            {
                return "Email confirmed successfully.";
            }

            return "the email confirmation failed";
        }
    }
}
