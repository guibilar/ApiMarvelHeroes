using MarvelHeroes.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarvelHeroes.Data.Mappings
{
    public class PersonagemMapping : IEntityTypeConfiguration<Personagem>
    {
        public void Configure(EntityTypeBuilder<Personagem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Guid)
                .IsUnique();

            builder.Property(p => p.IdMarvel)
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnType("TEXT")
                .HasMaxLength(int.MaxValue);

            builder.Property(p => p.LinkImagem)
                .HasMaxLength(300);

            builder.Property(p => p.LinkWiki)
                .HasMaxLength(300);

            builder.ToTable("personagem");
        }
    }
}