﻿using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using Table.DataAccess.Models;
using Table.DataAccess.Repositories.UnitOfWork;
using Table.Dto.Restaurant;

namespace Table.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RestaurantsController(
        IUnitOfWork unitOfWork,
        IWebHostEnvironment webHostEnvironment,
        IMapper mapper,
        IValidator<RestaurantDto> validator
        )
        : Controller
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
            ValidationResult result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                var res = mapper.Map<RestaurantOutputDto>(dto);
                return View(res);
            }

            if(file is not null)
            {
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
            }
            else
            {
                dto.ImageUrl = "";
            }

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

        public async Task<IActionResult> Delete(int id)
        {
            var restaurant = await unitOfWork.Restaurants.GetAsync(id);
            
            var viewModel = mapper.Map<RestaurantOutputDto>(restaurant);

            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var restaurant = await unitOfWork.Restaurants.GetAsync(id);
            if(restaurant is null)
                return NotFound();

            unitOfWork.Restaurants.Delete(restaurant);
            await unitOfWork.SaveAsync();

            return RedirectToAction("Index");

        }
    }
}
