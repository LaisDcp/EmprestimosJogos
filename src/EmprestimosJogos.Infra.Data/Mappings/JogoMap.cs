using EmprestimosJogos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimosJogos.Infra.Data.Mappings
{
    public class JogoMap : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.Property(c => c.Nome)
                .IsRequired();

            builder.HasOne(u => u.Creator)
                .WithMany(p => p.Jogos)
                .HasForeignKey(u => u.CreatorId);

            builder.Property(c => c.IsEmprestado)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasOne(u => u.Amigo)
                .WithMany(p => p.JogosEmprestados)
                .HasForeignKey(u => u.AmigoId)
                .IsRequired(false);
        }
    }
}
