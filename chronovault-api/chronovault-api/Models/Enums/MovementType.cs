using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum MovementType
    {
        [Description("Quartzo")]
        Quartz = 1,

        [Description("Automático")]
        Automatic = 2,

        [Description("Manual")]
        Manual = 3,

        [Description("Solar")]
        Solar = 4,

        [Description("Cinético")]
        Kinetic = 5,

        [Description("Digital")]
        Digital = 6,

        [Description("Híbrido")]
        Hybrid = 7
    }
}
