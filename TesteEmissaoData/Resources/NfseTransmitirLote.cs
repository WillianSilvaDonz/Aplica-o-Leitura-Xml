using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TesteEmissaoData.Resources
{
    [Serializable]
    public class NfseTransmitirLote
    {
        public string CodCidade { get; set; }

        public string NumeroLote { get; set; }

        public string Modulo { get; set; }

        public Prestador Prestador { get; set; }

        [XmlElement]
        public NfseTransmitir[] Notas { get; set; }

        //public NfseTransmitirLote ShallowCopy() => (NfseTransmitirLote) this.MemberwiseClone();
    }
}
