using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Abstract
{
    public interface IAdminService
    {
        public Task<List<UserDto>> GetAllUser(int PageSize, int PageNumber);
    }
}
