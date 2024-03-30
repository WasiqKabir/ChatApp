using BusinessEntities.Exceptions;
using BusinessEntities.BindingModels;
using BusinessEntities.ViewModels;
using Common;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessServices.Services.Account
{
    public class Account : IAccount
    {
        private readonly DataContext _dbcontext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing user-related data.</param>

        public Account(DataContext context)
        {
            _dbcontext = context;
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> Login(LoginBindingModel user)
        {
            if (user == null)
                throw new BusinessException(ErrorMessages.Common.InvalidRequest);
            
            var dbUser = await _dbcontext.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.IsAdmin == user.IsAdmin);

            if (dbUser == null)
                throw new BusinessException(ErrorMessages.Common.UserNotFound);

            bool isValid = Validate(dbUser, user);

            if (!isValid)
                throw new BusinessException(ErrorMessages.Validation.InvalidUser);

            return MapUser(dbUser);

        }

        /// <inheritdoc/>
        public async Task<UserViewModel> Register(SignupBindingModel user)
        {
            if (user == null || user.UserName == null || user.Password == null)
                throw new BusinessException(ErrorMessages.Validation.FieldsAreRequired);

            var isExist = await _dbcontext.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            if (isExist != null)
                throw new BusinessException(ErrorMessages.Common.UserNameIsTaken);

            User newUser = new()
            {
                UserId = new Guid(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                Lastname = user.Lastname,
                Password = user.Password,
                CreatedOn = DateTime.UtcNow,
                IsAdmin = user.IsAdmin
            };
            await _dbcontext.Users.AddAsync(newUser);
            await _dbcontext.SaveChangesAsync();

            return MapUser(newUser);


        }

        private bool Validate(User user, LoginBindingModel model)
        {
            if (user.UserName == model.UserName
                && user.Password == model.Password)
                return true;

            return false;
        }

        private UserViewModel MapUser(User user)
        {
            return new UserViewModel()
            {
                UserName = user.UserName,
                Password = user.Password,
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.Lastname,
                CreatedOn = user.CreatedOn,
                IsAdmin = user.IsAdmin
            };
        }
    }
}
