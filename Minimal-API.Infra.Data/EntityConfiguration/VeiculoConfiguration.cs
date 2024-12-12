using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal_API.Infra.Data.EntityConfiguration
{
    public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(n => n.Nome).IsRequired().HasMaxLength(150);
            builder.Property(m => m.Marca).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Ano).IsRequired();
        }
    }
}
