using Microsoft.AspNetCore.Mvc;
using System.IO;
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
        public DocumentoXml Emissao(NfseTransmitirLote lote)
        {

            DocumentoXml documento = _repository.Get(1);

            string xml = documento.Xml;
            xml = xml.Replace("#NumeroLote#", lote.NumeroLote);

            var caminhoCertificado = Path.Combine(Directory.GetCurrentDirectory(), "certificado.pfx");

            var certificate = new X509Certificate2(caminhoCertificado, "QUESTOR1234");

            SoapWebService soap = new SoapWebService("http://nfse1.publica.inf.br/chapeco_nfse_integracao/Services?wsdl", certificate);

            var resultSoap = soap.ExecuteMethod<string>("RecepcionarLoteRps", new object[] { xml });

            return documento;
        }
    }
}
