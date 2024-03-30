using BusinessEntities.ViewModels;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services.Users
{
    /// <summary>
    /// Represents a service for managing user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a list of non-admin users.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of user view models.</returns>

        public Task<List<UserViewModel>> GetAll();

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation that returns the user view model.</returns>
        /// /// <exception cref="BusinessException">Thrown when the user with the specified ID is not found.</exception>
        public Task<UserViewModel> GetByUserId(Guid userId);

        /// <summary>
        /// Retrieves a list of users excluding the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to exclude.</param>
        /// <returns>A task representing the asynchronous operation that returns a list of user view models.</returns>
        public Task<List<UserViewModel>> GetOtherUsers(Guid userId);

        /// <summary>
        /// Retrieves a list of users with whom the specified user has chatted.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation that returns a list of user view models.</returns>
        public Task<List<UserViewModel>> ChattedUser(string userId);
    }

    ///<inheritdoc/>
    public partial class UserService : IUserService
    {
        private readonly DataContext _dbContext;

        public UserService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
