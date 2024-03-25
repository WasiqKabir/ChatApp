using BusinessEntities.Exceptions;
using BusinessEntities.ViewModels;
using Common;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services.Users
{
    public partial class UserService 
    {
        public async Task<List<UserViewModel>> GetAll()
        {
            var usersQuery = _dbContext.Users.Where(x => !x.IsAdmin)
                .AsNoTracking();
            var users = await usersQuery.Take(30).ToListAsync();
            return UserListMapper(users);
        }

        public async Task<UserViewModel> GetByUserId(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                throw new BusinessException(ErrorMessages.Common.UserNotFound);

            return UserMapper(user);
        }

        public async Task<List<UserViewModel>> GetOtherUsers(Guid userId)
        {
            var friends = await _dbContext.Users.Where(x => x.UserId != userId && !x.IsAdmin)
                .AsNoTracking().ToListAsync();
            return UserListMapper(friends);

        }

        public async Task<List<UserViewModel>> ChattedUser(string userId)
        {
            Guid userGuid = Guid.Parse(userId);
            var chattedWithUsersQuery = _dbContext.Chats
                .Where(x => x.SenderId == userGuid)
                .OrderByDescending(x => x.LastChatted);

            var chattedWithUsers = await chattedWithUsersQuery.ToListAsync();

            var users = chattedWithUsers
                .Select(x => new UserViewModel
                { 
                    UserId = x.ReceiverId, 
                    FirstName = x.Receiver.FirstName,
                    LastName = x.Receiver.Lastname,
                    IsAdmin = x.Receiver.IsAdmin,
                    UserName = x.Receiver.UserName,
                }).ToList();

            return users;
        }

        private List<UserViewModel> UserListMapper(List<User> users)
        {
            return users.Select(user => new UserViewModel { UserId = user.UserId, UserName = user.UserName, FirstName = user.FirstName, LastName = user.Lastname, IsAdmin = user.IsAdmin, CreatedOn = user.CreatedOn }).ToList();
        }

        private UserViewModel UserMapper(User user)
        {
            return new UserViewModel()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.Lastname,
                UserName = user.UserName,
                IsAdmin = user.IsAdmin,
                CreatedOn = user.CreatedOn
            };
        }
    }
}
