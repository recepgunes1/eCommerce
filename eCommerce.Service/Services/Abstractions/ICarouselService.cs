using eCommerce.Entity.ViewModels.Carousel;

namespace eCommerce.Service.Services.Abstractions
{
    public interface ICarouselService
    {
        public Task AddCarouselSlideAsync(AddCarouselViewModel viewModel);
        public Task<IEnumerable<CarouselViewModel>> GetSlidesNonDeletedAsync();
    }
}
