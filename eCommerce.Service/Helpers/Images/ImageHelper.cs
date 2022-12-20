using eCommerce.Entity.ViewModels.Image;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Service.Helpers.Images
{
    public class ImageHelper : IImageHelper
    {
        private readonly IHostingEnvironment environment;
        private readonly string wwwroot;
        private const string imgFolder = "images";

        public ImageHelper(IHostingEnvironment environment)
        {
            this.environment = environment;
            wwwroot = environment.WebRootPath;
        }

        public void Delete(string name)
        {
            string fileToDelete = Path.Combine($"{wwwroot}/{imgFolder}/{name}");
            if (File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
            }
        }

        public async Task<UploadImageViewModel> UploadAsync(IFormFile image, string folder)
        {
            string folderPath = $"{wwwroot}/{imgFolder}/{folder}";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string extension = Path.GetExtension(image.FileName).ToLower();
            string newFileName = $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}{extension}";
            string imagePath = Path.Combine(folderPath, newFileName);
            await using FileStream stream = new(imagePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            await image.CopyToAsync(stream);
            await stream.FlushAsync();
            return new() { NameWithPath = $"{folder}/{newFileName}", Extension = extension };
        }
    }
}
