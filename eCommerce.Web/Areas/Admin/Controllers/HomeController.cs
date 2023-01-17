using eCommerce.Service.Services.Abstractions;
using eCommerce.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly ICommentService commentService;

        public HomeController(ICategoryService categoryService, IProductService productService, ICommentService commentService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminDashboardViewModel();
            viewModel.Category = await categoryService.CountCategoriesAsync();
            viewModel.Product = await productService.CountProductsAsync();
            viewModel.Comment = await commentService.CountCommentsAsync();
            return View(viewModel);
        }
    }
}
