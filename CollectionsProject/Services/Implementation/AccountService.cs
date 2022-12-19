using CollectionsProject.Models.UserModels;
using CollectionsProject.Services.Interfaces;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Implementation
{
    public class AccountService : IAccountService
    {
        public User CreateNewUser(RegisterViewModel regForm)
        {
            User newUser = new()
            {
                Email = regForm.Email,
                UserName = regForm.Name,
                Status = Status.Active,
                Role = Role.User,
            };
            return newUser;
        }
    }
}
