using EmprestimosJogos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimosJogos.Infra.Data.Mappings
{
    public class TokenMap : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.Property(e => e.Value)
                .HasColumnType("text")
                .IsRequired();

            builder.HasOne(u => u.Autenticacao)
                .WithMany(p => p.Tokens)
                .HasForeignKey(u => u.AutenticacaoId)
                .IsRequired();

            builder.HasOne(u => u.TokenType)
                .WithMany(p => p.Tokens)
                .HasForeignKey(u => u.TokenTypeId)
                .IsRequired();
        }
    }
}
