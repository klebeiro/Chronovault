using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum CaseSize
    {
        [Description("Pequeno (até 36mm)")]
        Small = 1,

        [Description("Médio (37-40mm)")]
        Medium = 2,

        [Description("Grande (41-44mm)")]
        Large = 3,

        [Description("Extra Grande (45mm+)")]
        ExtraLarge = 4
    }
}
