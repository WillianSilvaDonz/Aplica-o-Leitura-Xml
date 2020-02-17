namespace TesteEmissaoData.Resources
{
    public class Tomador
    {
        #region Public Properties

        public string RazaoSocial { get; set; }

        public string CPFCNPJ { get; set; }

        public string IM { get; set; }

        public string IE { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string IndicadorEstrangeiro { get; set; }

        public Endereco Endereco { get; set; }

        public string NomeFantasia { get; set; }

        public int CodigoMunicipio { get; set; }

        public bool DescontaRetencaoTotalNF { get; set; }

        public string TelefoneDDD { get; set; }

        #endregion

        #region Constructors

        public Tomador()
        {
            Endereco = new Endereco();
        }

        #endregion
    }
}