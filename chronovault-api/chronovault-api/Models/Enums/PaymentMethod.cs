using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum PaymentMethod
    {
        [Description("Cartão de Crédito")]
        CreditCard = 1,

        [Description("Cartão de Débito")]
        DebitCard = 2,

        [Description("PIX")]
        Pix = 3,

        [Description("Boleto Bancário")]
        BankSlip = 4,

        [Description("Transferência Bancária")]
        BankTransfer = 5,

        [Description("PayPal")]
        PayPal = 6,

        [Description("Dinheiro")]
        Cash = 7
    }
}
