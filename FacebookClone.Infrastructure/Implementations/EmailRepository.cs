using FacebookClone.Data.Entities;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace FacebookClone.Infrastructure.Implementations
{
    public class EmailRepository : IEmail
    {
        private readonly AppDb _appDb;
        public EmailRepository(AppDb appDb)
        {
            _appDb = appDb;
        }

        public async Task CreateAsync(OtpEmail otp)
        {
            await _appDb.OtpEmails.AddAsync(otp);
            await _appDb.SaveChangesAsync();
        }

        public async Task<OtpEmail?> GetLatestAsync(string userId)
        {
            return await _appDb.OtpEmails
                .Where(x => x.UserId == userId && !x.IsUsed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(OtpEmail otp)
        {
            _appDb.OtpEmails.Update(otp);
            await _appDb.SaveChangesAsync();
        }
    }
}