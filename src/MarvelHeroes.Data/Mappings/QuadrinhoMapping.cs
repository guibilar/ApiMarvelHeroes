using MarvelHeroes.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarvelHeroes.Data.Mappings
{
    public class QuadrinhoMapping : IEntityTypeConfiguration<Quadrinho>
    {
        public void Configure(EntityTypeBuilder<Quadrinho> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(p => p.Guid)
                   .IsUnique();

            builder.Property(c => c.IdMarvel)
                .IsRequired();

            builder.Property(c => c.Titulo)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(c => c.Preco)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Descricao)
                .HasColumnType("TEXT")
                .HasMaxLength(int.MaxValue);

            builder.Property(c => c.LinkImagem)
                .HasMaxLength(300);

            builder.Property(c => c.LinkWiki)
                .HasMaxLength(300);

            builder.ToTable("quadrinhos");
        }
    }
}