﻿namespace chronovault_api.DTOs
{
    public class CreditCardDTO
    {
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public int? CVV { get; set; }
        public int? ExpiryMonth { get; set; }
        public int? ExpiryYear { get; set; }
        public int? InstallmentCount { get; set; }
    }
}
