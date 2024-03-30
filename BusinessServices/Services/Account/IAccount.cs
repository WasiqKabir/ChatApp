using BusinessEntities.BindingModels;
using BusinessEntities.ViewModels;

namespace BusinessServices.Services.Account
{
    /// <summary>
    /// Represents a service for managing user account operations.
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// Authenticates a user by performing a login operation.
        /// </summary>
        /// <param name="user">The login credentials of the user.</param>
        /// <returns>A task representing the asynchronous operation that returns the user view model upon successful login.</returns>
        Task<UserViewModel> Login(LoginBindingModel user);

        /// <summary>
        /// Registers a new user by creating a user account.
        /// </summary>
        /// <param name="user">The signup details of the user.</param>
        /// <returns>A task representing the asynchronous operation that returns the user view model of the newly registered user.</returns>
        Task<UserViewModel> Register(SignupBindingModel user);
    }
}
