using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum WaterResistance
    {
        [Description("Não Resistente")]
        None = 0,

        [Description("30m - Respingos")]
        ThirtyMeters = 30,

        [Description("50m - Natação")]
        FiftyMeters = 50,

        [Description("100m - Snorkeling")]
        OneHundredMeters = 100,

        [Description("200m - Mergulho Recreativo")]
        TwoHundredMeters = 200,

        [Description("300m - Mergulho Profissional")]
        ThreeHundredMeters = 300,

        [Description("500m+ - Mergulho Extremo")]
        FiveHundredMetersPlus = 500
    }
}
