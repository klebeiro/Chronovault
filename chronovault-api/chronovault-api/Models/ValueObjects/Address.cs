namespace chronovault_api.Models.ValueObjects
{
    public class Address
    {
        public string Street { get; set; }
        public string? Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; } = "Brasil";
        public string? Complement { get; set; } 
    }
}
