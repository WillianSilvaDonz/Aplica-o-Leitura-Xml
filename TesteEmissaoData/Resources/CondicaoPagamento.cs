using EmissorNfse.Domain.Enums;
using System.Collections.Generic;

namespace TesteEmissaoData.Resources
{
    public class CondicaoPagamento
    {
        public TipoCondicaoPagamento TipoCondicaoPagamento { get; set; }

        public int QuantidadeParcelas { get; set; }

        public IList<ParcelaNota> Parcelas { get; set; } 
    }
}
