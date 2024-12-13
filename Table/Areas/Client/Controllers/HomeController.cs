using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Table.DataAccess.Repositories.UnitOfWork;
using Table.Dto.Restaurant;

namespace Table.Api.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var restaurants = await unitOfWork.Restaurants.GetAllAsync();
            var viewModels = mapper.Map<IEnumerable<RestaurantOutputDto>>(restaurants);
            return View(viewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
