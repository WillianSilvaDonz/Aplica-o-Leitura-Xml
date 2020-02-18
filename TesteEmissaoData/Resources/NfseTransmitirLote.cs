using System.Collections.Generic;

namespace TesteEmissaoData.Resources
{
    public class NfseTransmitirLote
    {
        public string NumeroLote { get; set; }

        public string Modulo { get; set; }

        public Prestador Prestador { get; set; }

        public IEnumerable<NfseTransmitir> Notas { get; set; }

        public NfseTransmitirLote ShallowCopy() => (NfseTransmitirLote) this.MemberwiseClone();
    }
}
