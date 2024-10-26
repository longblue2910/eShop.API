using AutoMapper;
using Basket.API.Entities;
using Shared.DTOs.Baskets;

namespace Basket.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CartDto, Cart>().ReverseMap();
        CreateMap<CartItemDto, CartItem>().ReverseMap();

    }
}
