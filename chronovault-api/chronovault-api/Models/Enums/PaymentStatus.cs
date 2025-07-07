using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum PaymentStatus
    {
        [Description("Pendente")]
        Pending = 1,

        [Description("Processando")]
        Processing = 2,

        [Description("Aprovado")]
        Approved = 3,

        [Description("Rejeitado")]
        Rejected = 4,

        [Description("Cancelado")]
        Cancelled = 5,

        [Description("Estornado")]
        Refunded = 6
    }
}
