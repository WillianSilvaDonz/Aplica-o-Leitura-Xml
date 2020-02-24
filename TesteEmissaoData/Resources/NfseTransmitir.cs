using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using EmissorNfse.Domain.Enum;
using EmissorNfse.Domain.Enums;

namespace TesteEmissaoData.Resources
{
    public class NfseTransmitir
    {
        #region Public Properties

        /// <summary>
        /// Protocolo de transação.
        /// Se informado no momento da transação o dados não serão submetidos novamente, apenas será consultado pelo lote com este protocolo.
        /// Caso o lote que esta sendo transmitido não for processado no tempo disponível para o operação informe este campo para continuar o processo em uma nova consulta.
        /// </summary>
        public string Protocolo { get; set; }

        public string Token { get; set; }

        /// <summary>
        /// Número da NFSE.
        /// </summary>
        //public string NumeroNota { get; set; }

        public string NumeroRps { get; set; }

        /// <summary>
        /// Usuário do Web Service.
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        /// Senha do Web Service.
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Código de Autorização para Emissão de Documentos Fiscais Eletrônicos
        /// </summary>
        public string CodigoAutorizacaoEmissaoDocumentosFiscais { get; set; }

        /// <summary>
        /// Série da NFSE.
        /// </summary>
        public string Serie { get; set; }

        /// <summary>
        /// Observação da NFSE.
        /// </summary>
        public string Observacao { get; set; }

        /// <summary>
        /// Data de emissão da NFSE.
        /// </summary>
        public DateTime DataEmissao { get; set; }

        /// <summary>
		/// Data de competência da NFSE
		/// </summary>
		public DateTime DataCompetencia { get; set; }

        /// <summary>
        /// Tipo de tributação pela Natureza de operação da prefeitura.
        /// </summary>
        public NaturezaOperacao NaturezaOperacao { get; set; }

        /// <summary>
        /// Situação do RPS
        /// </summary>
        public SituacaoRPS SituacaoRPS { get; set; }

        /// <summary>
        /// Incentivo Fiscal 
        /// </summary>
        [XmlIgnore]
        public IncentivoFiscal IncentivoFiscal { get; set; }

        /// <summary>
        /// Empresa optante pelo simples Nacional.
        /// </summary>
        [XmlIgnore]
        public Confirmacao OptanteSimplesNacional { get; set; }

        /// <summary>
        /// Empresa participante de incentivo cultural.
        /// </summary>
        [XmlIgnore]
        public Confirmacao IncentivadorCultural { get; set; }

        /// <summary>
        /// Tipo do regime de Tributação especial da empresa.
        /// </summary>
        public RegimeEspecialTributacao RegimeEspecialTributacao { get; set; }

        /// <summary>
        /// Tipo do RPS.
        /// </summary>
        [XmlIgnore]
        public Status Status { get; set; }

        /// <summary>
        /// Serviços prestados.
        /// </summary>
        public Servico Servico { get; set; }

        /// <summary>
        /// Pretador do serviço.
        /// </summary>
        //public Prestador Prestador { get; set; }

        /// <summary>
        /// Tomador do serviço.
        /// </summary>
        public Tomador Tomador { get; set; }

        public Obra Obra { get; set; }

        public IntermediarioServico IntermediarioServico { get; set; }


        /// <summary>
        /// Código identificado do lançamento
        /// </summary>
        public int CodigoLancamento { get; set; }

        /// <summary>
		/// Serviços prestados.
		/// </summary>
        [XmlElement]
        public Servico[] Servicos { get; set; }

        public PrestadorServico PrestadorServico { get; set; }


        /// <summary>
		/// Verifica a utilização da opção Tributação ISSQN NFS-e da natureza de estoque = 1
		/// </summary>
        public bool UsaTributacaoISSQNNfse1 { get; set; }

        public string? IdAssinatura { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public NfseTransmitir()
        {
            //Prestador = new Prestador();
            Tomador = new Tomador();
            Servico = new Servico();
            IntermediarioServico = new IntermediarioServico();
            PrestadorServico = new PrestadorServico();
        }

        #endregion
    }
}
