using FacebookClone.Core.Feature.Email.Command.Models;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Email.Command.Handlers
{
    public class SentEmaiCommandHandler : IRequestHandler<SentEmaiModel, string>
    {
        private readonly IEmailService _emailService;
        public SentEmaiCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task<string> Handle(SentEmaiModel request, CancellationToken cancellationToken)
        {
           return await _emailService.SendEmail(request.Email, request.Message);
          
        }
    }
}
