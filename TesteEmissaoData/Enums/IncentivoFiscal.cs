using System.ComponentModel.DataAnnotations;

namespace EmissorNfse.Domain.Enums
{
    public enum IncentivoFiscal
    {
        [Display(Name = "Optante do simples nacional inicio atividade")]
        OptanteSimplesNacionalInicioAtividade = 1,

        [Display(Name = "Serviço prestado minha casa minha vida")]
        ServicoPrestadoMinhaCasaMinhaVida = 2
    }
}