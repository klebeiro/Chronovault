using chronovault_api.Models.Enums;
using chronovault_api.Models.ValueObjects;

namespace chronovault_api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string? Phone { get; set; }

        public string? CPF { get; set; }

        public DateTime BirthDate { get; set; }

        public Address? Address { get; set; }

        public bool IsActive { get; set; } = true;
        public bool EmailConfirmed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLogin { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string? PreferredBrand { get; set; }

        public ProductGender? PreferredGender { get; set; }

        public decimal? MinimumPriceRange { get; set; }

        public decimal? MaximumPriceRange { get; set; }

        public bool AcceptsNewsletter { get; set; } = false;
        public bool AcceptsPromotions { get; set; } = false;
    }
}