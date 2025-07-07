using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum WatchCategory
    {
        [Description("Clássico")]
        Classic = 1,

        [Description("Esportivo")]
        Sport = 2,

        [Description("Casual")]
        Casual = 3,

        [Description("Formal")]
        Formal = 4,

        [Description("Luxo")]
        Luxury = 5,

        [Description("Smartwatch")]
        Smartwatch = 6,

        [Description("Vintage")]
        Vintage = 7,

        [Description("Mergulho")]
        Diving = 8
    }
}
