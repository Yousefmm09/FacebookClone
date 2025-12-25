using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Implementations;

namespace FacebookClone.Service.Abstract
{
    public interface IAuthenticationsService
    {
        Task<AuthMessage> CreatRefreshToken(string OldAccessToken, string RefreshToekn);
        Task<AuthMessage> CreateAccessTokenAsync(User user);
        Task<AuthMessage> ConfirmEmail(string userId, string token);
        Task<AuthMessage> ResetPassword(string userId, string token, string NewPassword);
        Task<string> CreateOtpAsync(string userId, string email);
        Task<string> VerifyOtpAsync(string userId, string code);
    }
}
