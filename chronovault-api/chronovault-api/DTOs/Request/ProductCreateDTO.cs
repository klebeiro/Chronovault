using chronovault_api.Models.Enums;

namespace chronovault_api.DTOs.Request
{
    public class ProductCreateDTO
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
        public WatchCategory Category { get; set; }
        public ProductGender Gender { get; set; }
        public MovementType MovementType { get; set; }
        public CaseMaterial CaseMaterial { get; set; }
        public StrapMaterial StrapMaterial { get; set; }
        public WaterResistance WaterResistance { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public bool IsOnSale { get; set; } = false;
    }
}