using EmprestimosJogos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimosJogos.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(c => c.Nome)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(l => l.PasswordHash)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasOne(u => u.Perfil)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(u => u.PerfilId);
        }
    }
}
