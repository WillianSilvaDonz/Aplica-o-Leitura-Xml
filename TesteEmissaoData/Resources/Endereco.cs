namespace TesteEmissaoData.Resources
{
	public class Endereco
	{
		#region Public Properties

		public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public int CodigoPais { get; set; }

        public int CodigoMunicipio { get; set; }

        public int CodigoSiafi { get; set; }

        public string DescricaoMunicipio { get; set; }

        public string UF { get; set; }

        public string CEP { get; set; }

        public string Cidade { get; set; }
        #endregion
    }
}