using chronovault_api.Models.Enums;

namespace chronovault_api.DTOs.Response
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public DateOnly BirthDate { get; set; }
        public AddressDTO Address { get; set; }
    }
}