using System.ComponentModel;

namespace chronovault_api.Models.Enums
{
    public enum ProductGender
    {
        [Description("Masculino")]
        Male = 1,

        [Description("Feminino")]
        Female = 2,

        [Description("Unissex")]
        Unisex = 3
    }
}
