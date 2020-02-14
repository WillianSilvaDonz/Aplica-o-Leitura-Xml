using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TesteEmissaoData.Data.Maps;
using TesteEmissaoData.Entities;

namespace TesteEmissaoData.Data
{
    public class StoreContextData : DbContext
    {
        public DbSet<DocumentoXml> DocumentoXml { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"server=localhost;Port=5432;user id=postgres;password=tuning;database=DbXml");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DocumentoXmlMap());
        }
    }
}
