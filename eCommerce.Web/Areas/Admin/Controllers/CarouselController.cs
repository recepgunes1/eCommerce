using eCommerce.Entity.ViewModels.Carousel;
using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CarouselController : Controller
    {
        private readonly ICarouselService carouselService;

        public CarouselController(ICarouselService carouselService)
        {
            this.carouselService = carouselService;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCarouselViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await carouselService.AddCarouselSlideAsync(viewModel);
                ModelState.Clear();
            }
            return View();
        }
    }
}
