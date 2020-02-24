using EmissorNfse.Module.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
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
            DocumentoXml documento = _repository.Get(4);
            string testeSerilize = XmlUtils.Serializar(lote, true);

            byte[] byteArray = Encoding.ASCII.GetBytes(testeSerilize);
            MemoryStream stream = new MemoryStream(byteArray);

            var oldDocument = XDocument.Load(stream);

            string xml = processarXml(documento.Xml, oldDocument).ToString();
            XmlDocument docAlimentado = new XmlDocument();
            docAlimentado.LoadXml(xml.ToString());
            var certificate = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "certificado.pfx"), "QUESTOR1234");
            
            XmlNodeList nodeAssinatura = docAlimentado.GetElementsByTagName("Assinatura");

            for (int i = (nodeAssinatura.Count - 1); i >= 0; i--) {
                XmlNode item = nodeAssinatura.Item(i);
                XmlDocument xmlSignature = XmlUtils.GetXmlDocument(XmlUtils.GetSignature(XmlUtils.GetXmlDocument(item.InnerXml), item.FirstChild.Attributes["id"].Value, certificate));
                XmlNode Signature = item.OwnerDocument.ImportNode(xmlSignature.DocumentElement, true);
                XmlNode XmlAssinado = item.OwnerDocument.ImportNode(XmlUtils.GetXmlDocument(item.InnerXml).DocumentElement, true);
                
                item.ParentNode.AppendChild(XmlAssinado);
                item.ParentNode.AppendChild(Signature);

                var xml_node = docAlimentado.GetElementsByTagName("Assinatura")[i];
                xml_node.ParentNode.RemoveChild(xml_node);

            }

            xml = docAlimentado.InnerXml;

            SoapWebService soap = new SoapWebService("http://nfse1.publica.inf.br/chapeco_nfse_integracao/Services?wsdl", certificate);

            var resultSoap = soap.ExecuteMethod<string>("RecepcionarLoteRps", new object[] { xml });

            return JsonConvert.SerializeXmlNode(XmlUtils.GetXmlDocument(resultSoap.Replace(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>", "")));
        }

        private XDocument processarXml(string xml, XDocument oldDocument)
        {
            var newDocument = new XDocument();
            using (var stringReader = new StringReader(xml))
            {
                using (XmlReader xsltReader = XmlReader.Create(stringReader))
                {
                    var transformer = new XslCompiledTransform();
                    transformer.Load(xsltReader);
                    using (XmlReader oldDocumentReader = oldDocument.CreateReader())
                    {
                        using (XmlWriter newDocumentWriter = newDocument.CreateWriter())
                        {
                            transformer.Transform(oldDocumentReader, newDocumentWriter);
                        }
                    }
                }
            }

            return newDocument;
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
