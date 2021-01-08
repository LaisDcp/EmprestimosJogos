using EmprestimosJogos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimosJogos.Infra.Data.Mappings
{
    public class TokenTypeMap : IEntityTypeConfiguration<TokenType>
    {
        public void Configure(EntityTypeBuilder<TokenType> builder)
        {
            builder.Property(e => e.Key)
               .HasMaxLength(4)
               .IsRequired(false);

            builder.Property(e => e.Value)
                .HasColumnType("text")
                .IsRequired();
        }
    }
}
