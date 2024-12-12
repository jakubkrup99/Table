using Microsoft.AspNetCore.Mvc;
using Table.DataAccess.Models;
using Table.DataAccess.Repositories.UnitOfWork;

namespace Table.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RestaurantsController(IUnitOfWork unitOfWork) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var restaurants = await unitOfWork.Restaurants.GetAllAsync();
            return View(restaurants);
        }
        
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            Restaurant restaurant = (id is null 
                ? new Restaurant()
                : await unitOfWork.Restaurants.GetAsync(id.Value))!;
            
            return View(restaurant);
        }
    }
}
