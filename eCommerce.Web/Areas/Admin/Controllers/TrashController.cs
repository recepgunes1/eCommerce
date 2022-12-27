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
        private readonly IUserService userService;

        public TrashController(IBrandService brandService, ICategoryService categoryService, ICommentService commentService, IProductService productService, IUserService userService)
        {
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.commentService = commentService;
            this.productService = productService;
            this.userService = userService;
        }

        public IActionResult Brands() => View();

        public async Task<IActionResult> GetBrands()
        {
            var brands = await brandService.GetAllBrandsDeletedAsync();
            return Json(brands.Select(p => new { p.Id, p.Name, p.CreatedDate, image = p.Image.NameWithPath }));
        }

        public IActionResult Categories() => View();

        public async Task<IActionResult> GetCategories()
        {
            var categories = await categoryService.GetAllCategoriesDeletedAsync();
            return Json(categories.Select(p => new { p.Id, p.Name, parentName = p.ParentCategory?.Name, p.CreatedDate }));
        }

        public IActionResult Comments() => View();

        public async Task<IActionResult> GetComments()
        {
            var comments = await commentService.GetAllCommentsDeletedAsync();
            return Json(comments.Select(p => new { p.Id, p.Content, p.CreatedDate, user = $"{p.User.FirstName} {p.User.LastName}", product = p.Product.Name, p.IsVisible }));
        }

        public IActionResult Products() => View();

        public async Task<IActionResult> GetProducts()
        {
            var products = await productService.GetAllProductsWithBrandAndCategoryDeletedAsync();
            return Json(products.Select(p => new { p.Id, p.Name, p.Price, p.Quantity, brand = p.Brand.Name, category = p.Category.Name, p.CreatedDate, image = p.Images.FirstOrDefault()?.NameWithPath })); ;
        }

        public IActionResult Users() => View();

        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetAllUsersWithRoleLockedOutAsync();
            return Json(users);
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
            await userService.RestoreUserAsync(id);
            return RedirectToAction("Users", "Trash", new { Area = "Admin" });
        }
    }
}
