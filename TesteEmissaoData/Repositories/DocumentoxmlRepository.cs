using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteEmissaoData.Data;
using TesteEmissaoData.Entities;

namespace TesteEmissaoData.Repositories
{
    public class DocumentoxmlRepository
    {
        private readonly StoreContextData _context;

        public DocumentoxmlRepository(StoreContextData context)
        {
            _context = context;
        }

        public IEnumerable<DocumentoXml> Get()
        {
            return _context.DocumentoXml;
        }

        public DocumentoXml Get(Int32 id)
        {
            return _context.DocumentoXml.Find(id);
        }

        public DocumentoXml GetCidade(string CodCidade)
        {
            return _context.DocumentoXml.Where(d => d.CodCidade == CodCidade).FirstOrDefault<DocumentoXml>();
        }

        public void Create(DocumentoXml documento)
        {
            _context.DocumentoXml.Add(documento);
            _context.SaveChanges();
        }
    }
}
