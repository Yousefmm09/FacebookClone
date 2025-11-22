using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Service.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Command.Handlers
{
    public class DeleteUserModelHandler : IRequestHandler<DeleteUserModel, string>
    {
        private readonly IUserService _userService;
        public DeleteUserModelHandler(IUserService userService)
        {
            _userService = userService;
        }
        public  async Task<string> Handle(DeleteUserModel request, CancellationToken cancellationToken)
        {
            var deleteUser = await _userService.DeleteUser();
            return deleteUser;
        }
    }
}
