using AutoMapper;
using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.DTOs;
using chronovault_api.Models.ValueObjects;
using chronovault_api.Models;
using chronovault_api.Models.Enums;

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
            CreateMap<Order, OrderResponseDetailsDTO>();
            CreateMap<Order, OrderResponseDTO>().ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderNumber.ToString()));
            CreateMap<OrderCreateDTO, Order>()
                .ForMember(dest => dest.ExpiryMonth, opt => opt.MapFrom(src =>
                src.PaymentMethod == PaymentMethod.CreditCard ? src.CreditCardInformation.ExpiryMonth : null))
            .ForMember(dest => dest.ExpiryYear, opt => opt.MapFrom(src =>
                src.PaymentMethod == PaymentMethod.CreditCard ? src.CreditCardInformation.ExpiryYear : null))
            .ForMember(dest => dest.CardholderName, opt => opt.MapFrom(src =>
                src.PaymentMethod == PaymentMethod.CreditCard ? src.CreditCardInformation.CardholderName : null))
            .ForMember(dest => dest.LastFourDigits, opt => opt.MapFrom(src =>
                src.PaymentMethod == PaymentMethod.CreditCard && !string.IsNullOrEmpty(src.CreditCardInformation.CardNumber) && src.CreditCardInformation.CardNumber.Length >= 4
                    ? src.CreditCardInformation.CardNumber.Substring(src.CreditCardInformation.CardNumber.Length - 4)
                    : null))
            .ForMember(dest => dest.InstallmentCount, opt => opt.MapFrom(src =>
                src.PaymentMethod == PaymentMethod.CreditCard ? src.CreditCardInformation.InstallmentCount : null));
            CreateMap<OrderUpdateDTO, Order>();
        
            // OrderItem
            CreateMap<OrderItem, OrderItemResponseDTO>()
                .ForMember(dest => dest.ProductInformation, opt => opt.MapFrom(src => src.Product));
            CreateMap<OrderItemCreateDTO, OrderItem>();
            CreateMap<OrderItemUpdateDTO, OrderItem>();

            // Address
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
        }
    }
}
