namespace chronovault_api.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountAmount { get; set; } = 0;

        public decimal TotalPrice
        {
            get
            {
                var total = (Price * Quantity) - DiscountAmount;
                return total < 0 ? 0 : total; 
            }
        }

        public bool IsValidQuantity()
        {
            return Quantity > 0;
        }

        public bool IsValidDiscount()
        {
            return DiscountAmount >= 0 && DiscountAmount <= (Price * Quantity);
        }

        public bool IsValidOrderItem()
        {
            return IsValidQuantity() && IsValidDiscount() && Price > 0;
        }
    }
}