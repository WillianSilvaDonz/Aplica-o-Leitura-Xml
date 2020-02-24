using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace EmissorNfse.Domain.Enums
{
    public enum SituacaoRPS
    {
        [XmlEnum("Tributado prestador")]
        [Display(Name = "Tributado prestador")]
        TributadaNoPrestador = 1,

        [XmlEnum("Tributado tomador")]
        [Display(Name = "Tributado tomador")]
        TributadaNoTomador = 2,

        [XmlEnum("Tributado fixo")]
        [Display(Name = "Tributado fixo")]
        TributadoFixo = 3,

        [XmlEnum("Isenta/Imune")]
        [Display(Name = "Isenta/Imune")]
        IsentaImune = 4,

        [XmlEnum("Outro município")]
        [Display(Name = "Outro município")]
        OutroMunicípio = 5,

        [XmlEnum("Exportação")]
        [Display(Name = "Exportação")]
        Exportacao = 6,

        [XmlEnum("Cancelada")]
        [Display(Name = "Cancelada")]
        Cancelada = 7
    }
}