using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookClone.Data.Entities;
using FacebookClone.Service.Dto;

namespace FacebookClone.Service.Abstract
{
    public interface IEmailService
    {
        public Task<string> SendEmail(string email, string message);
    }
}
