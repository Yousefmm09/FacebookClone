using FacebookClone.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Abstract
{
    public  interface IEmail
    {
        Task CreateAsync(OtpEmail otp);
        Task<OtpEmail?> GetLatestAsync(string userId);
        Task UpdateAsync(OtpEmail otp);
    }
}