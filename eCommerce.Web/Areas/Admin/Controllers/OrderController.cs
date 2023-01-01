using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly IShoppingSessionService shoppingSessionService;
        private readonly IUserService userService;

        public OrderController(IShoppingSessionService shoppingSessionService, IUserService userService)
        {
            this.shoppingSessionService = shoppingSessionService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrders()
        {
            Func<Guid, string> GetUserFullName = id =>
            {
                var user = shoppingSessionService.GetOwnerByIdAsync(id).Result;
                return $"{user.FirstName} {user.LastName}";
            };
            var orders = await shoppingSessionService.GetAllOldOrdersAsync();
            return Json(orders.Select(p => new { p.Id, user = GetUserFullName(p.UserId), p.CreatedDate, products = p.Products.Select(i => new { name = i.Name, brand = i.Brand.Name, i.Price }), total = p.Products.Sum(p => p.Price) }));
        }
    }
}
