using eCommerce.Entity.ViewModels.Image;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Service.Helpers.Images
{
    public interface IImageHelper
    {
        Task<UploadImageViewModel> UploadAsync(IFormFile image, string folder);
        void Delete(string name);
    }
}
