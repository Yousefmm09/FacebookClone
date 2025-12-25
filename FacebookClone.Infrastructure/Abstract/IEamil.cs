using FacebookClone.Data.Entities;

namespace FacebookClone.Infrastructure.Abstract
{
    public interface IEmail
    {
        Task CreateAsync(OtpEmail otp);
        Task<OtpEmail?> GetLatestAsync(string userId);
        Task UpdateAsync(OtpEmail otp);
    }
}