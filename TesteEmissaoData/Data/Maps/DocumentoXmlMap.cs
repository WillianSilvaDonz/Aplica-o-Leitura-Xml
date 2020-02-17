using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TesteEmissaoData.Entities;

namespace TesteEmissaoData.Data.Maps
{
    public class DocumentoXmlMap : IEntityTypeConfiguration<DocumentoXml>
    {
        public void Configure(EntityTypeBuilder<DocumentoXml> builder)
        {
            builder.ToTable("documentoxml");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Xml).IsRequired().HasColumnType("text").HasColumnName("xml");
            builder.Property(x => x.Cidade).IsRequired().HasMaxLength(100).HasColumnType("character(100)").HasColumnName("cidade");
            builder.Property(x => x.CodCidade).IsRequired().HasMaxLength(10).HasColumnType("character(10)").HasColumnName("codcidade");
        }
    }
}
