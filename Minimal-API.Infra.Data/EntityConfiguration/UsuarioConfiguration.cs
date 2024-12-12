using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal_API.Domain.Entities;

namespace Minimal_API.Infra.Data.EntityConfiguration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(n => n.Nome).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
            builder.Property(pass => pass.PasswordSalt).IsRequired(false);
            builder.Property(pass => pass.PasswordHash).IsRequired(false);
            builder.Property(p => p.Perfil).IsRequired().HasMaxLength(255);
        }
    }
}
