using EmprestimosJogos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimosJogos.Infra.Data.Mappings
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.Property(e => e.Key)
                .HasMaxLength(4)
                .IsRequired(false);

            builder.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Descricao)
                .HasMaxLength(150)
                .IsRequired(false);

        }
    }
}