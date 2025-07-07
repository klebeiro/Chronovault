using chronovault_api.Models.Enums;

namespace chronovault_api.DTOs.Request
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public List<OrderItemCreateDTO> OrderItems { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal DiscountAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? ShippingAddress { get; set; }
    }
}
