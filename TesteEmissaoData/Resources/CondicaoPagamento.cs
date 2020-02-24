using EmissorNfse.Domain.Enums;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TesteEmissaoData.Resources
{
    public class CondicaoPagamento
    {
        public TipoCondicaoPagamento TipoCondicaoPagamento { get; set; }

        public int QuantidadeParcelas { get; set; }

        [XmlElement]
        public ParcelaNota[] Parcelas { get; set; } 
    }
}
