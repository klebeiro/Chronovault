namespace chronovault_api.DTOs.Response
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public UserResponseDTO User { get; set; }    
    }
}
