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
            builder.Property(p => p.Id_marvel)
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnType("TEXT")
                .HasMaxLength(int.MaxValue);

            builder.Property(p => p.Pic_url)
                .HasMaxLength(300);

            builder.Property(p => p.Wiki_url)
                .HasMaxLength(300);

            builder.ToTable("personagem");
        }
    }
}