using chronovault_api.Models.Enums;
using chronovault_api.Models.ValueObjects;

namespace chronovault_api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }
        public Address ShippingAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public DateTime? PaymentDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Dados do cartão
        public string? CardholderName { get; set; }
        public string? LastFourDigits { get; set; }
        public string? PaymentToken { get; set; }
        public int? ExpiryMonth { get; set; }
        public int? ExpiryYear { get; set; }
        public int? InstallmentCount { get; set; }
    }
}