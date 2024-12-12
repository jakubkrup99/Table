using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Table.DataAccess.Models;
using Table.DataAccess.Repositories.UnitOfWork;
using Table.Dto.Restaurant;

namespace Table.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RestaurantsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var restaurants = await unitOfWork.Restaurants.GetAllAsync();
            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantOutputDto>>(restaurants);
            return View(restaurantsDtos);
        }
        
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            Restaurant restaurant = (id is null 
                ? new Restaurant()
                : await unitOfWork.Restaurants.GetAsync(id.Value))!;
            
            var restaurantDto = mapper.Map<RestaurantOutputDto>(restaurant);
            return View(restaurantDto);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(RestaurantDto dto, IFormFile? file)
        {
            if(file is null) throw new ArgumentNullException(nameof(file));
            string wwwRootPath = webHostEnvironment.WebRootPath;
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var restaurantPath = Path.Combine(wwwRootPath, @"images\restaurants\");

            if(!string.IsNullOrWhiteSpace(dto.ImageUrl))
            {
                var oldImage = Path.Combine(wwwRootPath, dto.ImageUrl.TrimStart('\\'));
                if(System.IO.File.Exists(oldImage))
                    System.IO.File.Delete(oldImage);
            }

            using(var fileStream = new FileStream(Path.Combine(restaurantPath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            dto.ImageUrl = $@"\images\restaurants\{fileName}";
            var restaurant = mapper.Map<Restaurant>(dto);
            if(restaurant.Id == 0)
            {
                await unitOfWork.Restaurants.AddAsync(restaurant);
                TempData["success"] = "Restaurant Added";
            }
            else
            {
                unitOfWork.Restaurants.Update(restaurant);
                TempData["success"] = "Restaurant Updated";

            }
            await unitOfWork.SaveAsync();
            return RedirectToAction("Index");


        }
    }
}
