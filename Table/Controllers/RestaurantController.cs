using Microsoft.AspNetCore.Mvc;

namespace Table.Api.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
