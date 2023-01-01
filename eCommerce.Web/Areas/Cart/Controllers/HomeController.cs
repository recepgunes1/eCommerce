using eCommerce.Service.Services.Abstractions;
using eCommerce.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Card.Controllers
{
    [Area("Cart")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICartService cartService;
        private readonly IShoppingSessionService shoppingSessionService;

        public HomeController(ICartService cartService, IShoppingSessionService shoppingSessionService)
        {
            this.cartService = cartService;
            this.shoppingSessionService = shoppingSessionService;
        }

        public IActionResult Cart()
        {
            return View();
        }

        public async Task<IActionResult> GetProducts()
        {
            var session = await shoppingSessionService.GetCurrentShoppingSessionAsync();
            var products = await cartService.GetProductsAsync(session.Id);
            return Json(products.Select(p => new { p.Id, p.Name, p.Price, brand = p.Brand.Name, category = p.Category.Name, image = p.Images.FirstOrDefault()?.NameWithPath }));
        }

        public async Task<IActionResult> Payment()
        {
            var session = await shoppingSessionService.GetCurrentShoppingSessionAsync();
            ViewBag.Price = await cartService.GetTotalPrice(session.Id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Payment(PaymentViewModel paymentViewModel)
        {
            var session = await shoppingSessionService.GetCurrentShoppingSessionAsync();
            if (ModelState.IsValid)
            {
                var experie = new DateTime(paymentViewModel.Year, paymentViewModel.Month, 1);
                if (experie > DateTime.Today)
                {
                    await shoppingSessionService.CompleteShoppingAsync(session.Id);
                    return RedirectToAction("Index", "Home", new { Area = "" });
                }
                else
                {
                    ModelState.AddModelError("", "Card experied.");
                    return View();

                }
            }
            ViewBag.Price = await cartService.GetTotalPrice(session.Id);
            ModelState.AddModelError("", "Something went wrong.");
            return View();
        }

        public async Task<IActionResult> AddProductToCart(Guid productId)
        {
            var session = await shoppingSessionService.GetCurrentShoppingSessionAsync();
            await cartService.AddProductToCartAsync(productId, session.Id);
            return Ok();
        }

        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            var session = await shoppingSessionService.GetCurrentShoppingSessionAsync();
            await cartService.DeleteProductFromCartAsync(productId, session.Id);
            var anyProduct = await cartService.AnyProductInCartAsync(session.Id);
            if (!anyProduct)
            {
                await shoppingSessionService.CloseSessionAsync();
            }
            return RedirectToAction("Cart", "Home", new { Area = "Cart" });
        }
    }
}
