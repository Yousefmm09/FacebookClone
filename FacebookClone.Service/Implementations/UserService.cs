using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendService _friendService;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(IUserRepository userRepository,IHttpContextAccessor httpContextAccessor,IFriendService friendService)

        {
            _userRepository = userRepository;
            _contextAccessor = httpContextAccessor;
            _friendService = friendService;
        }
        public async Task<string> DeleteUser()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");

            // هجيب IDS الأصدقاء
            var friends = await _friendService.getAllFriends(userId);

            // امسح كل الصداقات (friend ↔ user)
            foreach (var friendId in friends)
            {
                await _friendService.RemoveBothFriendShips(userId, friendId);
            }

            // امسح كل طلبات الصداقة المرسلة / المستلمة
            await _friendService.RemoveFriendRequests(userId);

            // امسح المستخدم
            return await _userRepository.DeleteUser(userId);
        }



        public async Task<List<Friendship>> GetFriends(string userId)
        {
            return await _userRepository.GetFriends(userId);
        }

        public async Task<ProfileUserDto> GetProfile()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");

            var user = await _userRepository.GetProfile(userId);

            var likeCount =  _userRepository.GetLikeCount(userId);
            var friendCount =  _userRepository.GetFriendCount(userId);

            return new ProfileUserDto
            {
                Name = user.UserName,
                Bio = user.Bio,
                ProfilePic = user.ProfilePictureUrl,
                Email = user.Email,
                Likes = likeCount,
                Friends = friendCount,
                Posts = user.Posts.Select(p => new UserPostDto
                {
                    Content = p.Content,
                    CreatedAt = p.CreatedAt
                }).ToList()
            };
        }

        public async Task<string> UpdateProfile( User profileData)
        {
            var user = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(user))
                throw new Exception("User not authenticated");
            return  await _userRepository.UpdateProfile(user, profileData);
        }
    }
}
