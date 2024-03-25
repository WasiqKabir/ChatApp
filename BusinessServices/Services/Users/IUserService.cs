using BusinessEntities.ViewModels;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services.Users
{
    public interface IUserService
    {
        public Task<List<UserViewModel>> GetAll();
        public Task<UserViewModel> GetByUserId(Guid userId);
        public Task<List<UserViewModel>> GetOtherUsers(Guid userId);
        public Task<List<UserViewModel>> ChattedUser(string userId);
    }

    public partial class UserService : IUserService
    {
        private readonly DataContext _dbContext;

        public UserService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
