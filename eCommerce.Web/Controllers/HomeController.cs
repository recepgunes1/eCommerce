using eCommerce.Entity.ViewModels.Product;
using eCommerce.Service.Services.Abstractions;
using eCommerce.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ICarouselService carouselService;
        private readonly IBrandService brandService;
        private readonly ICommentService commentService;

        public HomeController(ICategoryService categoryService, IProductService productService, ICarouselService carouselService, IBrandService brandService, ICommentService commentService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.carouselService = carouselService;
            this.brandService = brandService;
            this.commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel();
            homeViewModel.ParentCategories = await categoryService.GetAllParentCategoriesNonDeletedAsync();
            homeViewModel.Slides = await carouselService.GetSlidesNonDeletedAsync();
            homeViewModel.SuggestedBrands = await brandService.GetSuggestedBrandsAsync();
            homeViewModel.SuggestedProducts = await productService.GetSuggestedProductsAsync();
            return View(homeViewModel);
        }

        [Route("/Category/{id}")]
        [Route("/Category/{id}/{pageId:int:min(1)}")]
        public async Task<IActionResult> ProductsFromCategory(Guid id, int pageId = 1)
        {
            var category = await categoryService.GetCategoryByGuidAsync(id);
            var products = await productService.GetAllProductsWithBrandAndCategoryToCategoryNonDeletedAsync(category);
            if (products.Count() == 0)
            {
                return View("Products", products);
            }
            else if (products.Count() > 0)
            {
                var chunkedProducts = products.Chunk(20);
                return View("Products", chunkedProducts.ElementAt(pageId - 1));

            }
            return NotFound();
        }

        [Route("/Brand/{id}")]
        [Route("/Brand/{id}/{pageId:int:min(1)}")]
        public async Task<IActionResult> ProductsFromBrand(Guid id, int pageId = 1)
        {
            var brand = await brandService.GetBrandByGuidAsync(id);
            var products = await productService.GetAllProductsWithBrandAndCategoryToBrandNonDeletedAsync(brand);
            if (products.Count() == 0)
            {
                return View("Products", products);
            }
            else if (products.Count() > 0)
            {
                var chunkedProducts = products.Chunk(20);
                return View("Products", chunkedProducts.ElementAt(pageId - 1));

            }
            return NotFound();
        }

        [Route("/Product/{id}")]
        public async Task<IActionResult> Product(Guid id)
        {
            var product = await productService.GetProductWithCommentsByGuidAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> GetSubCategories(Guid id)
        {
            var children = await categoryService.GetAllSubCategoriesToParentGuidNonDeletedAsync(id);
            return Json(children);
        }

        [Authorize]
        public async Task<IActionResult> AddComment(ProductWithCommentsViewModel viewModel)
        {
            var comment = viewModel.AddComment;
            await commentService.AddCommentAsync(comment);
            return RedirectToAction("Product", new { id = comment.ProductId });
        }
    }
}
