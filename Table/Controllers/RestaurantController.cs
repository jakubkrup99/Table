using Microsoft.AspNetCore.Mvc;
using Table.DataAccess.Repositories.UnitOfWork;

namespace Table.Api.Controllers
{
    public class RestaurantController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var restaurants = await unitOfWork.Restaurants.GetAllAsync();
            return View(restaurants);
        }
    }
}
