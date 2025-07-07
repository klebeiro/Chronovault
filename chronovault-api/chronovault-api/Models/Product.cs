using chronovault_api.Models.Enums;

namespace chronovault_api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public WatchCategory? Category { get; set; }

        public ProductGender? Gender { get; set; }

        public MovementType? MovementType { get; set; }

        public string? Description { get; set; }

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

        public decimal? OriginalPrice { get; set; }

        public decimal DiscountPercentage => OriginalPrice.HasValue && OriginalPrice > 0
            ? Math.Round(((OriginalPrice.Value - Price) / OriginalPrice.Value) * 100, 2)
            : 0;

        public bool IsInStock => StockQuantity > 0;

        public bool HasDiscount => OriginalPrice.HasValue && OriginalPrice > Price;

        public bool IsValidForSale()
        {
            return IsActive && IsInStock && Price > 0;
        }
    }
}