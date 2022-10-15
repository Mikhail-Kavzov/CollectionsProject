using CollectionsProject.Hash;
using CollectionsProject.Services.Interfaces;

namespace CollectionsProject.Services.Implementation
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static readonly string pathToFolder = "/images/";
        private const string noPhoto = "noPhoto.jpg";

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> CreateFileAsync(IFormFile file)
        {
            string fileName = MyGuid.GetGuid() + file.FileName;
            string path = pathToFolder + fileName;
            using var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create); //create file
            await file.CopyToAsync(fileStream); //save asynchroniously
            return fileName;
        }

        public void DeleteFile(string fileName)
        {
            string path = pathToFolder + fileName;
            File.Delete(_webHostEnvironment.WebRootPath + path);
        }

        public Task<string> ReadFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateFileAsync(IFormFile file, string? oldFileName)
        {
            var newFileName = await CreateFileAsync(file);
            if (oldFileName!=noPhoto) //if file exists
            File.Delete(_webHostEnvironment.WebRootPath + pathToFolder + oldFileName);
            return newFileName;
        }
    }
}
