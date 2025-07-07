namespace chronovault_api.Models
{
    public class UserCredential
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}