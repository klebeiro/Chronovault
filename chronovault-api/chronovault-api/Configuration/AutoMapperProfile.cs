using AutoMapper;
using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.DTOs;
using chronovault_api.Models.ValueObjects;
using chronovault_api.Models;

namespace chronovault_api.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();

            // Product
            CreateMap<Product, ProductResponseDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();

            // Order
            CreateMap<Order, OrderResponseDTO>();
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderUpdateDTO, Order>();

            // OrderItem
            CreateMap<OrderItem, OrderItemResponseDTO>();
            CreateMap<OrderItemCreateDTO, OrderItem>();
            CreateMap<OrderItemUpdateDTO, OrderItem>();

            // Image
            CreateMap<Image, ImageResponseDTO>();
            CreateMap<ImageCreateDTO, Image>();
            CreateMap<ImageUpdateDTO, Image>();

            // Address
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
        }
    }
}
