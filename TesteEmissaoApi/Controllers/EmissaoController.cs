using EmissorNfse.Module.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TesteEmissaoData.Entities;
using TesteEmissaoData.Repositories;
using TesteEmissaoData.Resources;
using TesteEmissaoData.Services;

namespace TesteEmissaoApi.Controllers
{
    
    public class EmissaoController : Controller
    {
        private readonly DocumentoxmlRepository _repository;
        public EmissaoController(DocumentoxmlRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/emissao")]
        [HttpPost]
        public string Emissao(NfseTransmitirLote lote)
        {
            DocumentoXml documento = _repository.GetCidade(lote.Modulo);

            string xml = documento.Xml;
            xml = xml.Replace("@NumeroLote", lote.NumeroLote);
            
            xml = xml.Replace("@IdAssinatura", verificaIdAssinatura(lote));

            string xmlsignature = GetValorTag(xml.Replace("@Assinatura",""), "Rps");

            var certificate = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "certificado.pfx"), "QUESTOR1234");
            string Signature = XmlUtils.GetSignature(XmlUtils.GetXmlDocument(xmlsignature), verificaIdAssinatura(lote), certificate);

            xml = xml.Replace("@Assinatura", Signature);

            SoapWebService soap = new SoapWebService("http://nfse1.publica.inf.br/chapeco_nfse_integracao/Services?wsdl", certificate);

            var resultSoap = soap.ExecuteMethod<string>("RecepcionarLoteRps", new object[] { xml });

            return JsonConvert.SerializeXmlNode(XmlUtils.GetXmlDocument(resultSoap.Replace(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>", "")));
        }

        [Route("v1/consultar/{protocolo}")]
        [HttpGet]
        public object Consultar(string protocolo)
        {

            //DocumentoXml documento = _repository.Get(3);

            string xml = @"<ConsultarLoteRpsEnvio xmlns=""http://www.publica.inf.br""><Prestador id=""assinar""><Cnpj>79011862000170</Cnpj><InscricaoMunicipal>8178</InscricaoMunicipal></Prestador><Protocolo>"+protocolo+"</Protocolo></ConsultarLoteRpsEnvio>";

            var caminhoCertificado = Path.Combine(Directory.GetCurrentDirectory(), "certificado.pfx");

            var certificate = new X509Certificate2(caminhoCertificado, "QUESTOR1234");
            string Signature = XmlUtils.GetSignature(XmlUtils.GetXmlDocument(xml), "assinar", certificate);

            xml = xml.Replace("</Prestador>", "</Prestador>" + Signature);

            SoapWebService soap = new SoapWebService("http://nfse1.publica.inf.br/chapeco_nfse_integracao/Services?wsdl", certificate);

            string resultSoap = soap.ExecuteMethod<string>("ConsultarLoteRps", new object[] { xml });
            resultSoap = JsonConvert.SerializeXmlNode(XmlUtils.GetXmlDocument(resultSoap.Replace(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>", "")));

            return resultSoap;
        }

        private string verificaIdAssinatura(NfseTransmitirLote lote)
        {
            NfseTransmitir nota = getNotaNumero(lote);
            return (nota.IdAssinatura != null) ? nota.IdAssinatura : "rps" + lote.NumeroLote;
        }

        private NfseTransmitir getNotaNumero(NfseTransmitirLote lote)
        {
            return lote.Notas.Where(x => x.NumeroRps == lote.NumeroLote).First<NfseTransmitir>();
        }

        private string GetValorTag(string xml, string name)
        {

            if (xml != null)
            {

                if (!string.IsNullOrEmpty(xml) && xml.Contains("<" + name + ">"))
                {

                    var xmlRetorno = xml.Remove(0, xml.IndexOf("<" + name + ">"));

                    xmlRetorno = xmlRetorno.Remove(xmlRetorno.IndexOf("</" + name + ">")).Replace("<" + name + ">", "");

                    return xmlRetorno;

                }

            }

            return string.Empty;
        }
    }
}
