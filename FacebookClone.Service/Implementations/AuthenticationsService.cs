using Azure.Core;
using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Infrastructure.Context;
using FacebookClone.Infrastructure.Implementations;
using FacebookClone.Service.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static FacebookClone.Service.Implementations.AuthMessage;

namespace FacebookClone.Service.Implementations
{
    public class AuthenticationsService : IAuthenticationsService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthenticationsService(UserManager<User> userManager,IConfiguration configuration,IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;   
        }

        public async Task<AuthMessage> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var confirmEmail = await _userManager.ConfirmEmailAsync(user, token);
                if (confirmEmail != null)
                {
                    return new AuthMessage { Message = "the email is confirmed" };
                }
                else
                    return new AuthMessage { Message = "the email is confirmed" };
            }
            return new AuthMessage { Message = "not found user" };
                    
        }

        public async Task<AuthMessage> CreateAccessTokenAsync(User user)
        {
            var token= await GenerateJwtToken(user);
            var rtoken =  CreatRefreshToken(user);
            var jwttoken = new UserRefreshToken
            {
                AccessToken = token.AccessToken,
                RefreshTokenSecret = rtoken.TokenString,
                Expired = rtoken.ExpireDate,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };
             await _refreshTokenRepository.RefreshToken(jwttoken);
            return new AuthMessage
            {
                AccessToken = token.AccessToken,
                refreshToken = rtoken,
                Message = "Tokens generated successfully."
            };
        }
        public async Task<AuthMessage> CreatRefreshToken( string OldAccessToken, string RefreshToekn )
        {
            var jwttoken= new JwtSecurityTokenHandler().ReadJwtToken(OldAccessToken);
           if(jwttoken==null|| jwttoken.Header.Alg!=SecurityAlgorithms.HmacSha256)
                throw new ArgumentException("Invalid access token.");

            if (jwttoken.ValidTo > DateTime.UtcNow)
                throw new ArgumentException("The access token has not expired yet.");
            var userId= jwttoken.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Invalid token payload.");
            var savedToken= _refreshTokenRepository.GetTableNoTracking().FirstOrDefault(x=>x.AccessToken==OldAccessToken&&
            x.RefreshTokenSecret==RefreshToekn&&x.UserId==userId);
            if (savedToken == null)
                throw new ArgumentException("Refresh token not found or invalid.");

            if (savedToken.Expired <= DateTime.UtcNow)
                throw new ArgumentException("Refresh token has expired.");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentException("User not found.");
            var NewAccessToken = await CreateAccessTokenAsync(user);
            var NewRefreshToken=  CreatRefreshToken(user);
            var userRToken = new UserRefreshToken
            {
                AccessToken=NewAccessToken.AccessToken,
                RefreshTokenSecret=NewRefreshToken.TokenString,
                UserId=userId,
                CreatedAt=DateTime.UtcNow,
                Expired=NewRefreshToken.ExpireDate,
            };
            await _refreshTokenRepository.RefreshToken(userRToken);

            return new AuthMessage
            {
                AccessToken = NewAccessToken.AccessToken,
                refreshToken = NewRefreshToken,
                Message = "Access token refreshed successfully."
            };
        }
        private RefreshToken CreatRefreshToken(User user)
        {
            var random= new byte[32];
            using var generateRandom= RandomNumberGenerator.Create();
            generateRandom.GetBytes(random);
            return new RefreshToken
            {
                UserName=user.UserName,
                ExpireDate=DateTime.UtcNow.AddMinutes(10),
                TokenString= Convert.ToBase64String(random)
            };
        }
        private async Task<AuthMessage> GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            // creat sign
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //creat claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
            };
            //creat token
            var token = new JwtSecurityToken
                (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: sign
                );
            var accesstoken = new JwtSecurityTokenHandler().WriteToken(token);
            return new AuthMessage
            {
                AccessToken = accesstoken
            };
        }
    }
}
