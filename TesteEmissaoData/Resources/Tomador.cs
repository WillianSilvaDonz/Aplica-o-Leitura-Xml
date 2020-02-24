namespace TesteEmissaoData.Resources
{
    public class Tomador
    {
        private bool _CnpjNulo;
        private string _CPFCNPJ;

        #region Public Properties

        public string RazaoSocial { get; set; }

        public string CPFCNPJ {
            get { return _CPFCNPJ; }
            set { 
                _CPFCNPJ = value; 
                _CnpjNulo = ((value.Length > 12) ? ((value == "00000000000000" || value == "99999999999999") ? true : false) : ((value == "00000000000" || value == "99999999999") ? true : false)); 
            }
        }

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

        public bool CnpjNulo {
            get { return _CnpjNulo; }
            set { 
                _CnpjNulo = value; 
            }
        }

        #endregion

        #region Constructors

        public Tomador()
        {
            Endereco = new Endereco();
        }

        #endregion
    }
}