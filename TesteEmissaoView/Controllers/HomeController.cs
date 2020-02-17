using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TesteEmissaoData.Entities;
using TesteEmissaoData.Repositories;
using TesteEmissaoView.Models;

namespace TesteEmissaoView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DocumentoxmlRepository _repository;

        public HomeController(ILogger<HomeController> logger, DocumentoxmlRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //IEnumerable<DocumentoXml> documento = _repository.Get();
            return View();
        }

        [HttpPost]
        [Route("home/savexml")]
        public IActionResult saveXml(string xml, string cidade, string codcidade)
        {
            DocumentoXml teste = new DocumentoXml()
            {
                Xml = xml,
                Cidade = cidade,
                CodCidade = codcidade
            };
            _repository.Create(teste);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
