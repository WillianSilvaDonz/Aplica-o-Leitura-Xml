using System.ComponentModel.DataAnnotations;

namespace EmissorNfse.Domain.Enums
{
    public enum SituacaoRPS
    {
        [Display(Name = "Tributado prestador")]
        TributadaNoPrestador = 1,

        [Display(Name = "Tributado tomador")]
        TributadaNoTomador = 2,

        [Display(Name = "Tributado fixo")]
        TributadoFixo = 3,

        [Display(Name = "Isenta/Imune")]
        IsentaImune = 4,

        [Display(Name = "Outro município")]
        OutroMunicípio = 5,

        [Display(Name = "Exportação")]
        Exportacao = 6,

        [Display(Name = "Cancelada")]
        Cancelada = 7
    }
}