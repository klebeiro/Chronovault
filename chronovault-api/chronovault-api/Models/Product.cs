using chronovault_api.Models.Enums;

namespace chronovault_api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public List<string> Images { get; set; } = new List<string>();

        public WatchCategory? Category { get; set; }

        public ProductGender? Gender { get; set; }

        public MovementType? MovementType { get; set; }


        public int StockQuantity { get; set; } = 0;

        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public bool IsOnSale { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public CaseSize? CaseSize { get; set; }

        public CaseMaterial? CaseMaterial { get; set; }

        public StrapMaterial? StrapMaterial { get; set; }

        public WaterResistance? WaterResistance { get; set; }

    }
}