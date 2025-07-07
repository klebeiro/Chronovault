using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum CaseMaterial
    {
        [Description("Aço Inoxidável")]
        StainlessSteel = 1,

        [Description("Ouro")]
        Gold = 2,

        [Description("Ouro Rosa")]
        RoseGold = 3,

        [Description("Prata")]
        Silver = 4,

        [Description("Titânio")]
        Titanium = 5,

        [Description("Alumínio")]
        Aluminum = 6,

        [Description("Cerâmica")]
        Ceramic = 7,

        [Description("Plástico")]
        Plastic = 8,

        [Description("Bronze")]
        Bronze = 9,

        [Description("Carbono")]
        Carbon = 10
    }
}
