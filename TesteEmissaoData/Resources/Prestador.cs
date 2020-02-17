namespace TesteEmissaoData.Resources
{
	public class Prestador
	{
		#region Public Properties

        public string RazaoSocial { get; set; }

        public string CPFCNPJ { get; set; }

        public string CMC { get; set; }

        public string IM { get; set; }

        public int CodigoMunicipio { get; set; }

        public int CodigoPais { get; set; }

        public int CodigoSiafi { get; set; }

        public int CCM { get; set; }

        public Endereco Endereco { get; set; }

        public string Email { get; set; }

        public int InscricaoMobiliaria { get; set; }

        public string telefone { get; set; }

        public string telefoneDDD { get; set; }

        #endregion

        #region Constructors

        public Prestador()
        {
            Endereco = new Endereco();
        }

        #endregion
    }
}