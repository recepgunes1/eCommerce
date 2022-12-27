using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Card.Controllers
{
    [Area("Cart")]
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
