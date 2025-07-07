using chronovault_api.Models.Enums;

namespace chronovault_api.DTOs.Response
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressDTO? Address { get; set; }
        public string? PreferredBrand { get; set; }
        public ProductGender? PreferredGender { get; set; }
        public decimal? MinimumPriceRange { get; set; }
        public decimal? MaximumPriceRange { get; set; }
        public bool AcceptsNewsletter { get; set; }
        public bool AcceptsPromotions { get; set; }
    }
}