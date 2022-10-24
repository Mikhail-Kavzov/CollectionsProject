using CollectionsProject.ViewModels;

namespace CollectionsProject.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeModel> GetHomeModel(int countCollection=5,int countItem=5,int countTags=20);
    }
}
