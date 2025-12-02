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
        public AdminService(IAdmin adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<List<UserDto>> GetAllUser(int PageSize, int PageNumber)
        {
            var user = await _adminRepository.GetAllUser(PageSize, PageNumber);
            var totalCount = await _adminRepository.GetTotalUsersCountAsync();

            return user.Select(x => new UserDto
            {
                Id=x.Id,
                UserName=x.UserName,
                CreatedAt=x.CreatedAt,
                Email=x.Email
            }).ToList();
        }
    }
}
