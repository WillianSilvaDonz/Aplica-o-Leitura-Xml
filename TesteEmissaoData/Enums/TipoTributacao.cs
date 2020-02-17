using System.ComponentModel.DataAnnotations;

namespace EmissorNfse.Domain.Enums
{
    public enum TipoTributacao
    {
        [Display(Name = "Isenta de ISS")]
        IsentaDeISS,

        [Display(Name = "Não incidência no município")]
        NaoIncidenciaNoMunicipio,

        [Display(Name = "Imune")]
        Imune,

        [Display(Name = "ExigibiliddSuspDecJProcA")]
        ExigibiliddSuspDecJProcA,

        [Display(Name = "Não tributável")]
        NaoTributavel,

        [Display(Name = "Tributável")]
        Tributavel,

        [Display(Name = "Tributável fixo")]
        TributavelFixo,

        [Display(Name = "Tributável SN")]
        TributavelSN,

        [Display(Name = "Microempreendedor individual")]
        MicroEmpreendedorIndividual
    }
}