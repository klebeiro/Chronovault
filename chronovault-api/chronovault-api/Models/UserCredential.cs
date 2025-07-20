namespace chronovault_api.Models
{
    public class UserCredential
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User"; 
    }
}