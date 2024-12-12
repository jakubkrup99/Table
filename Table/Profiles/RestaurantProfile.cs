using AutoMapper;
using Table.DataAccess.Models;
using Table.Dto.Restaurant;

namespace Table.Api.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<RestaurantDto, Restaurant>();
            CreateMap<Restaurant, RestaurantOutputDto>();
        }
    }
}
