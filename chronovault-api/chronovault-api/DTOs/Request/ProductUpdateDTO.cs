using chronovault_api.Models.Enums;

namespace chronovault_api.DTOs.Request
{
    public class ProductUpdateDTO
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public WatchCategory Category { get; set; }
        public ProductGender Gender { get; set; }
        public MovementType MovementType { get; set; }
        public string? CaseMaterial { get; set; }
        public string? StrapMaterial { get; set; }
        public string? WaterResistance { get; set; }
        public string? Warranty { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsOnSale { get; set; }
    }
}