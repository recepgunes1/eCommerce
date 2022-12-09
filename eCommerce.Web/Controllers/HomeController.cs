using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService product;

        public HomeController(IProductService product)
        {
            this.product = product;
        }

        public async Task<IActionResult> Index()
        {
            var products = await product.GetAllProductsAsync();
            return View(products);
        }
    }
}
