using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Card.Controllers
{
    [Area("Cart")]
    [Authorize(Roles = "customer")]
    public class HomeController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }
    }
}
