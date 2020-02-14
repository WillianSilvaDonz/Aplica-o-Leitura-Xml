using System;
using System.Collections.Generic;
using System.Text;

namespace TesteEmissaoData.Entities
{
    public class DocumentoXml
    {
        public int Id { get; set; }
        public string Xml { get; set; }
        public string Cidade { get; set; }
        public string CodCidade { get; set; }
    }
}
