using Microsoft.AspNetCore.Http;

namespace eCommerce.Entity.ViewModels.Carousel
{
    public class AddCarouselViewModel
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
}
