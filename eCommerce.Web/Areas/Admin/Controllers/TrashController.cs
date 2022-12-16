using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class TrashController : Controller
    {
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly ICommentService commentService;
        private readonly IProductService productService;

        public TrashController(IBrandService brandService, ICategoryService categoryService, ICommentService commentService, IProductService productService)
        {
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.commentService = commentService;
            this.productService = productService;
        }

        public async Task<IActionResult> Brands()
        {
            var brands = await brandService.GetAllBrandsDeletedAsync();
            return View(brands);
        }
        public async Task<IActionResult> Categories()
        {
            var categories = await categoryService.GetAllCategoriesDeletedAsync();
            return View(categories);
        }

        public async Task<IActionResult> Comments()
        {
            var comments = await commentService.GetAllCommentsDeletedAsync();
            return View(comments);
        }

        public async Task<IActionResult> Products()
        {
            var products = await productService.GetAllProductsWithBrandAndCategoryDeletedAsync();
            return View(products);
        }

        public async Task<IActionResult> Users()
        {
            return View();
        }

        public async Task<IActionResult> RestoreBrand(Guid id)
        {
            await brandService.RestoreBrandAsync(id);
            return RedirectToAction("Brands", "Trash", new { Area = "Admin" });
        }

        public async Task<IActionResult> RestoreCategory(Guid id)
        {
            await categoryService.RestoreCategoryAsync(id);
            return RedirectToAction("Categories", "Trash", new { Area = "Admin" });
        }

        public async Task<IActionResult> RestoreComment(Guid id)
        {
            await commentService.RestoreCommentAsync(id);
            return RedirectToAction("Comments", "Trash", new { Area = "Admin" });
        }

        public async Task<IActionResult> RestoreProduct(Guid id)
        {
            await productService.RestoreProductAsync(id);
            return RedirectToAction("Products", "Trash", new { Area = "Admin" });
        }

        public async Task<IActionResult> RestoreUser(Guid id)
        {
            return RedirectToAction("Users", "Trash", new { Area = "Admin" });
        }
    }
}
