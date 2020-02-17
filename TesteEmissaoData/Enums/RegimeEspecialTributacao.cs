using System.ComponentModel.DataAnnotations;

namespace EmissorNfse.Domain.Enums
{
    public enum RegimeEspecialTributacao
    {
        [Display(Name = "Nenhum")]
        Nenhum = 0,

        [Display(Name = "Microempresa")]
        Microempresa = 1,

        [Display(Name = "Estimativa")]
        Estimativa = 2,

        [Display(Name = "Sociedade")]
        Sociedade = 3,

        [Display(Name = "Cooperativa")]
        Cooperativa = 4,

        [Display(Name = "ME Simples nacional")]
        MEISimplesNacional = 5,

        [Display(Name = "ME e EPP Simples nacional")]
        MEEPPSimplesNacional = 6
    }
}
