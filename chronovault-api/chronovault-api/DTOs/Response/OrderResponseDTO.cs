using chronovault_api.Models.Enums;

namespace chronovault_api.DTOs.Response
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public string? OrderNumber { get; set; }
        public List<OrderItemResponseDTO> OrderItems { get; set; } = new List<OrderItemResponseDTO>();
    }
}
