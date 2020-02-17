using System.ComponentModel.DataAnnotations;

namespace EmissorNfse.Domain.Enums
{
    public enum Status
    {
        [Display(Name = "Normal")]
        Normal = 1,

        [Display(Name = "Cancelado")]
        Cancelado = 2
    }
}
