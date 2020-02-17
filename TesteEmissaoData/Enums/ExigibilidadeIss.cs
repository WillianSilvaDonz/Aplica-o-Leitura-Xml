using System.ComponentModel.DataAnnotations;

namespace EmissorNfse.Domain.Enums
{
    public enum ExigibilidadeIss
    {
        [Display(Name = "Exigivel")]
        Exigivel = 1,

        [Display(Name = "Não incidência")]
        Naoincidencia = 2,

        [Display(Name = "Isenção")]
        Isencao = 3,

        [Display(Name = "Exportação")]
        Exportacao = 4,

        [Display(Name = "Imunidade")]
        Imunidade = 5,

        [Display(Name = "Suspenda por decisão judícial")]
        SuspensaPorDecisaoJudicial = 6,

        [Display(Name = "Suspensa por processo administrativo")]
        SuspensaPorProcessoAdministrativo = 7
    }
}