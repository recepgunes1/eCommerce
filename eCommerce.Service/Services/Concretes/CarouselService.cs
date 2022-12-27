using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Carousel;
using eCommerce.Service.Helpers.Images;
using eCommerce.Service.Services.Abstractions;

namespace eCommerce.Service.Services.Concretes
{
    public class CarouselService : ICarouselService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IImageHelper imageHelper;

        public CarouselService(IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.imageHelper = imageHelper;
        }
        public async Task AddCarouselSlideAsync(AddCarouselViewModel viewModel)
        {
            var image = await imageHelper.UploadAsync(viewModel.File, "carousel");
            var mappedImage = mapper.Map<Image>(image);
            await unitOfWork.GetRepository<Image>().AddAsync(mappedImage);

            var mappedSlide = mapper.Map<Carousel>(viewModel);
            mappedSlide.ImageId = mappedImage.Id;
            await unitOfWork.GetRepository<Carousel>().AddAsync(mappedSlide);
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CarouselViewModel>> GetSlidesNonDeletedAsync()
        {
            var slides = await unitOfWork.GetRepository<Carousel>().GetAllAsync(p => !p.IsDeleted, i => i.Image);
            var mappedSlides = mapper.Map<IEnumerable<CarouselViewModel>>(slides.OrderByDescending(p => p.CreatedDate)).Take(3);
            return mappedSlides;
        }
    }
}
