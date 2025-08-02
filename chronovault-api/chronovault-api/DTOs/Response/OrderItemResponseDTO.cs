namespace chronovault_api.DTOs.Response
{
    public class OrderItemResponseDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductResponseDTO ProductInformation { get; set; }
    }
}
