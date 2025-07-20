namespace chronovault_api.DTOs.Request
{
    public class UserCreateDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public DateOnly BirthDate { get; set; }
        public AddressDTO Address { get; set; }
    }
}