﻿namespace chronovault_api.DTOs
{
    public class AddressDTO
    {
        public string Street { get; set; }
        public string? Number { get; set; } 
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string? Complement { get; set; }
    }
}
