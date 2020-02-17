using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmissorNfse.Domain.Enums
{
    public enum NaturezaOperacao
    {
        [Display(Name = "Tributação no municipio")]
        TributacaoNoMunicipio = 0,

        [Display(Name = "Tributação fora do municipio")]
        TributacaoForaMunicipio = 1,

        [Display(Name = "Isenção")]
        Isencao = 2,

        [Display(Name = "Imune")]
        Imune = 3,

        [Display(Name = "Suspensão judicial")]
        SuspensaoJudicial = 4,

        [Display(Name = "Suspensão administrativa")]
        SuspensaoAdministrativa = 5,

        [Display(Name = "Não tributável")]
        NaoTributavel = 58
    }
}
