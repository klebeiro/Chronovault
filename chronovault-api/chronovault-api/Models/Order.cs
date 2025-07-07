using chronovault_api.Models.Enums;
using chronovault_api.Models.ValueObjects;

namespace chronovault_api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string OrderNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal TotalAmount { get; set; }

        public decimal ShippingCost { get; set; }

        public decimal TaxAmount { get; set; }

        private decimal _discountAmount;

        public decimal DiscountAmount
        {
            get { return _discountAmount; }
            set
            {
                if (value > TotalAmount)
                {
                    throw new ArgumentException("O desconto não pode ser maior que o valor total.");
                }
                _discountAmount = value;
            }
        }

        public Address ShippingAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public DateTime? PaymentDate { get; set; }
        public string Notes { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public bool IsValidShippingAddress()
        {
            return ShippingAddress != null &&
                   !string.IsNullOrEmpty(ShippingAddress.Street) &&
                   !string.IsNullOrEmpty(ShippingAddress.City) &&
                   !string.IsNullOrEmpty(ShippingAddress.ZipCode);
        }
    }
}