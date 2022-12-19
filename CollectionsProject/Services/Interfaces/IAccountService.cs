using CollectionsProject.Models.UserModels;
using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Interfaces
{
    public interface IAccountService
    {
        User CreateNewUser(RegisterViewModel regForm);
    }
}
