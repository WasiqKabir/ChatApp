using BusinessEntities.BindingModels;
using BusinessEntities.ViewModels;

namespace BusinessServices.Services.Account
{
    public interface IAccount
    {
        public Task<UserViewModel> Login(LoginBindingModel user);
        public Task<UserViewModel> Register(SignupBindingModel user);
    }
}
