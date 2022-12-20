using eCommerce.Service.Services.Abstractions;
using eCommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public HomeController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel();
            homeViewModel.ParentCategories = await categoryService.GetAllParentCategoriesNonDeletedAsync();
            return View(homeViewModel);
        }

        public async Task<IActionResult> Products(Guid id)
        {
            var category = await categoryService.GetCategoryByGuidAsync(id);
            var products = await productService.GetAllProductsWithBrandAndCategoryToCategoryNonDeletedAsync(category);
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> GetSubCategories(Guid id)
        {
            var children = await categoryService.GetAllSubCategoriesToParentGuidNonDeletedAsync(id);
            return Json(children);
        }
    }
}
