using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum StrapMaterial
    {
        [Description("Couro")]
        Leather = 1,

        [Description("Aço Inoxidável")]
        StainlessSteel = 2,

        [Description("Silicone")]
        Silicone = 3,

        [Description("Nylon")]
        Nylon = 4,

        [Description("Borracha")]
        Rubber = 5,

        [Description("Tecido")]
        Fabric = 6,

        [Description("Titânio")]
        Titanium = 7,

        [Description("Cerâmica")]
        Ceramic = 8,

        [Description("Ouro")]
        Gold = 9,

        [Description("Prata")]
        Silver = 10
    }
}
