using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EmissorNfse.Domain.Enums
{
    public enum TipoCondicaoPagamento
    {
        [Display(Name = "Á vista")]
        AVista,

        [Display(Name = "Á prazo")]
        APrazo,

        [Display(Name = "Outros")]
        Outros
    }
}
