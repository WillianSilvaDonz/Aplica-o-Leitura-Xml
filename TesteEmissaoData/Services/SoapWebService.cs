using System.Net;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using EmissorNfse.Module.Utils;
using System.IO;
using EmissorNfse.SoapModules.Utils;
using System;
using System.Xml;

namespace TesteEmissaoData.Services
{
    public class SoapWebService
    {
        #region Private Readonly Fields
        /// <summary>
        /// Certificado de autenticação do WebService.
        /// </summary>
        private readonly X509Certificate2 certificado;

        /// <summary>
        /// WSDL do WebService
        /// </summary>
        private readonly string wsdl;

        #endregion

        #region Constructors
        public SoapWebService(string wsdl, X509Certificate2 certificado)
        {
            this.certificado = certificado;
            this.wsdl = wsdl;

        }
        /// <summary>
        /// Constrói e envia a mensagem Soap
        /// </summary>
        /// <param name="action">Soap Action que identifica a operação do Web Service a ser executada</param>
        /// <param name="body">Conteúdo do Body da mensagem Soap</param>
        /// <returns>O conteúdo do Body da mensagem Soap de retorno</returns>
        #endregion

        #region Private Methods
        private string CallWebService(string action, string body)
        {
            string soapResult = "";
            try
            {
                var soapEnvelopeXml = CreateSoapEnvelope(body);
                var webRequest = CreateWebRequest(action);
                var stream = webRequest.GetRequestStream();
                soapEnvelopeXml.Save(stream);
                var response = webRequest.GetResponse() as HttpWebResponse;
                soapResult = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    soapResult = soapResult.ToXDocument().Root.Value.ToString().Replace("&lt;", "<").Replace("&gt;", ">");
                }
                else
                {
                    throw new Exception(soapResult);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    throw new Exception(ex.Message);
                }
                var stream = ex.Response.GetResponseStream();
                var reader = new StreamReader(stream);
                var error = reader.ReadToEnd();
                throw new Exception(error);
            }
            return soapResult;
        }

        /// <summary>
		/// Cria e prepara o objeto HttpWebRequest a ser utilizado no envio da mensagem Soap
		/// </summary>
		/// <param name="action">Soap Action que identifica a operação do Web Service a ser executada</param>
		/// <returns>Uma instância de HttpWebRequest</returns>
        private HttpWebRequest CreateWebRequest(string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(wsdl);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.ClientCertificates.Add(certificado);
            webRequest.UserAgent = "Client Cert Sample";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        /// <summary>
		/// Cria uma mensagem Soap a partir de um conteúdo Body recebido.
		/// </summary>
		/// <param name="action">Body da mensagem Soap</param>
		/// <returns>Uma instância de XmlDocument contendo a mensagem Soap</returns>
        private XmlDocument CreateSoapEnvelope(string body)
        {
            return XmlUtils.GetXmlDocument("<S:Envelope xmlns:S=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ns2=\"http://service.nfse.integracao.ws.publica/\"><S:Body><ns2:RecepcionarLoteRps><XML>" +
                body +
                "</XML></ns2:RecepcionarLoteRps></S:Body></S:Envelope>");
        }
        #endregion

        #region Public Override Methods
        /// <summary>
        /// Executa um método do WebService usando o protocolo Soap
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de retorno.</typeparam>
        /// <param name="methodName">Nome do método que será executado.</param>
        /// <param name="parameters">Parametros do método que será executado.</param>		
        /// <returns>Instancia de T.</returns>
        public T ExecuteMethod<T>(string methodName, params object[] parameters)
        {
            var res = (object)CallWebService(methodName, parameters[0].ToString());
            return (T)res;
        }
        #endregion
    }
}
