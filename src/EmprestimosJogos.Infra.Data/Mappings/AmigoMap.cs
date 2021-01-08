using EmprestimosJogos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimosJogos.Infra.Data.Mappings
{
    public class AmigoMap : IEntityTypeConfiguration<Amigo>
    {
        public void Configure(EntityTypeBuilder<Amigo> builder)
        {
            builder.Property(c => c.Nome)
                .IsRequired();

            builder.Property(c => c.CEP)
                .HasMaxLength(8)
                .IsRequired(false);

            builder.Property(c => c.Logradouro)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(c => c.Complemento)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.Bairro)
                .HasMaxLength(60)
                .IsRequired(false);

            builder.Property(c => c.Numero)
                .HasMaxLength(9)
                .IsRequired(false);

            builder.Property(c => c.TelefoneCelular)
                .HasMaxLength(14)
                .IsRequired();

            builder.HasOne(u => u.Usuario)
                .WithMany(p => p.Amigos)
                .HasForeignKey(u => u.UsuarioId);
        }
    }
}
