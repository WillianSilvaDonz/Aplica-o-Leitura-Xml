using System.ComponentModel.DataAnnotations;

namespace EmissorNfse.Domain.Enums
{
	public enum Confirmacao
	{
        [Display(Name = "Sim")]
        Sim = 1,

        [Display(Name = "Não")]
        Nao = 2
	}
}