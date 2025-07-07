using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum OrderStatus
    {
        [Description("Pendente")]
        Pending = 1,

        [Description("Confirmado")]
        Confirmed = 2,

        [Description("Processando")]
        Processing = 3,

        [Description("Enviado")]
        Shipped = 4,

        [Description("Entregue")]
        Delivered = 5,

        [Description("Cancelado")]
        Cancelled = 6,

        [Description("Devolvido")]
        Returned = 7
    }
}
