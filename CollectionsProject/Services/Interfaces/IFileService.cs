namespace CollectionsProject.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> CreateFileAsync(IFormFile file);
        void DeleteFile(string fileName);
        Task<string> UpdateFileAsync(IFormFile file,string oldFileName);
        Task<string> ReadFileAsync(string fileName);
    }
}
