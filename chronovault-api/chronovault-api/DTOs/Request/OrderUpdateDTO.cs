using chronovault_api.Models.Enums;

namespace chronovault_api.DTOs.Request
{
    public class OrderUpdateDTO
    {
        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal DiscountAmount { get; set; }
        public string? ShippingAddress { get; set; }
    }
}
