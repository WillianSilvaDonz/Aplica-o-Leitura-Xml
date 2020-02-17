using EmissorNfse.Domain.Enums;
using System.Collections.Generic;

namespace TesteEmissaoData.Resources
{
    public class Servico
    {
        private string _valoriss;

        private string _valorliquidonfse;

        private string _aliquota;

        private string _basecalculo;

        #region Public Properties

        public string ItemListaServico { get; set; }

        public Confirmacao IssRetido { get; set; }

        public TipoTributacao TipoTributacao { get; set; }

        public ExigibilidadeIss ExigibilidadeIss { get; set; }

        public IEnumerable<CondicaoPagamento> CondicaoPagamentoList { get; set; }

        public string NumeroProcesso { get; set; }

        public string Discriminacao { get; set; }

        public string CodigoMunicipioTributacaoServico { get; set; }

        public string CodigoTributacaoMunicipio { get; set; }

        public int CodigoMunicipioPrestacaoServico { get; set; }

        public int CodigoFiscalPrestacaoServico { get; set; }

        public int CodigoCnae { get; set; }

        public string BaseCalculo {
            get { return _basecalculo; }
            set { _basecalculo = addPoint(value); }
        }

        public decimal ValorServico { get; set; }

        public decimal ValorDeducoes { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorIr { get; set; }

        public string ValorIss {
            get { return _valoriss; }
            set { _valoriss = addPoint(value); }
        }

        public decimal ValorIssRetido { get; set; }

        public decimal ValorPis { get; set; }

        public decimal ValorCofins { get; set; }

        public decimal ValorInss { get; set; }

        public decimal ValorCsll { get; set; }

        public decimal AliquotaPis { get; set; }

        public decimal AliquotaCofins { get; set; }

        public decimal AliquotaInss { get; set; }

        public decimal AliquotaIr { get; set; }

        public decimal AliquotaCsll { get; set; }

        public string ValorLiquidoNfse {
            get { return _valorliquidonfse; }
            set { _valorliquidonfse = addPoint(value); }
        }

        public decimal DescontoIncondicionado { get; set; }

        public decimal DescontoCondicionado { get; set; }

        public string Aliquota {
            get { return _aliquota; }
            set { _aliquota = addPoint(value); }
        }

        public decimal OutrasRetencoes { get; set; }

        public string Unidade { get; set; }

        public string DescricaoServico { get; set; }

        public int CodigoMunicipio { get; set; }

        public int ItemServico { get; set; }

        public string CodigoSiafi { get; set; }

        #endregion

        private string addPoint(string valor)
        {
            return valor.Insert(valor.Length - 2, ".");
        }
    }
}