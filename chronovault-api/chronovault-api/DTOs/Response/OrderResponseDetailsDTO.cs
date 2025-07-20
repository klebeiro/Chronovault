using chronovault_api.Models.Enums;
using chronovault_api.Models.ValueObjects;

namespace chronovault_api.DTOs.Response
{
    public class OrderResponseDetailsDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int UserId { get; set; }
        public List<OrderItemResponseDTO> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Address ShippingAddress { get; set; }
        public string CardholderName { get; set; }
        public string LastFourDigits { get; set; }
        public string PaymentToken { get; set; }
        public int? ExpiryMonth { get; set; }
        public int? ExpiryYear { get; set; }
        public int? InstallmentCount { get; set; }
    }
}
