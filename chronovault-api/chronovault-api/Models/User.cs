using chronovault_api.Models.Enums;
using chronovault_api.Models.ValueObjects;

namespace chronovault_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public DateOnly BirthDate { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}