using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IAdmin _adminRepository;
        private readonly IPostRepository _postRepository;
        private readonly 
        public AdminService(IAdmin adminRepository, IPostRepository postRepository)
        {
            _adminRepository = adminRepository;
            _postRepository = postRepository;
        }
        public async Task<List<UserDto>> GetAllUser(int PageSize, int PageNumber)
        {
            var user = await _adminRepository.GetAllUser(PageSize, PageNumber);
            var totalCount = await _adminRepository.GetTotalUsersCountAsync();

            return user.Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                CreatedAt = x.CreatedAt,
                Email = x.Email
            }).ToList();
        }
        public async Task<MessageDto> BannedUser(string userId,string BannedReason)
        {
            var user= await _adminRepository.BannedUser(userId,BannedReason);
            if (user == true)
                return new MessageDto
                {
                    Message = "The user is banned Successfully"
                };
            return new MessageDto { Message = "The user is not banned" };
        }
        public async Task<MessageDto> UnbannedUser(string userId)
        {
            var user= await _adminRepository.UnbannedUser(userId);
            if (user == true)
                return new MessageDto
                {
                    Message = "The user is unbanned Successfully"
                };
            return new MessageDto { Message = "The user is not unbanned" };
        }

        public async Task<UserDto> GetUserDetails(string userId)
        {
            var getuser= await _adminRepository.GetUserDetails(userId);
            var countPost= await _postRepository.GetCountPostbyUser(userId);
            var CountFriendShip=
            if(getuser!=null)
            {
                return new UserDto
                {
                    UserName=getuser.UserName,
                    Email=getuser.Email,
                    PostCount=countPost,
                    FrinedCount=
                };
            }
        }
    }
}
