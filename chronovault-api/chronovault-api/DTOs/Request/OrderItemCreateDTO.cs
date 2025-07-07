namespace chronovault_api.DTOs.Request
{
    public class OrderItemCreateDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
