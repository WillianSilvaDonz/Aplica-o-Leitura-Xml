using System.Net;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using EmissorNfse.Module.Utils;
using System.IO;
using EmissorNfse.SoapModules.Utils;
using System;

namespace TesteEmissaoData.Services
{
    public class SoapWebService
    {
        #region Private Readonly Fields

        private readonly X509Certificate2 _certificado;

        private readonly IConfigurationSection _configuration;

        #endregion

        #region Constructors
        public SoapWebService(IConfigurationSection configuration, X509Certificate2 certificado)
        {
            _certificado = certificado;
            _configuration = configuration;

        }
        #endregion

        #region Private Methods
        private string CallWebService(string action, string body)
        {
            string soapResult;
            try
            {
                var envelope = _configuration.GetSection($"{action}:Envelope").Value;
                var soapEnvelopeXml = XmlUtils.GetXmlDocument(envelope.Replace("%#BODY#%", body));
                var webRequest = CreateWebRequest(action);
                var stream = webRequest.GetRequestStream();
                soapEnvelopeXml.Save(stream);
                var response = webRequest.GetResponse() as HttpWebResponse;
                soapResult = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    soapResult = soapResult.ToXDocument().Root.Value;
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

        private HttpWebRequest CreateWebRequest(string action)
        {
            var wsdl = _configuration.GetSection("Wsdl").Value;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(wsdl);

            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ClientCertificates.Add(_certificado);
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.ContentType = @"text/xml;charset=""utf-8""";

            return webRequest;
        }

        #endregion

        #region Public Override Methods
        public string ExecuteMethod(string methodName, string body)
        {
            return CallWebService(methodName, body);
        }
        #endregion
    }
}
