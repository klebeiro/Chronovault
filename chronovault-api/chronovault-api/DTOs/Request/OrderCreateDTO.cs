using chronovault_api.Models.Enums;

namespace chronovault_api.DTOs.Request
{
    public class OrderCreateDTO
    {
        public List<OrderItemCreateDTO> OrderItems { get; set; } = new List<OrderItemCreateDTO>();
        public CreditCardDTO? CreditCardInformation { get; set;}
        public PaymentMethod PaymentMethod { get; set; }
        public AddressDTO ShippingAddress { get; set; }
    }
}
