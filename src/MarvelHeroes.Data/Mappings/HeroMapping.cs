using MarvelHeroes.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarvelHeroes.Data.Mappings
{
    public class HeroMapping : IEntityTypeConfiguration<Hero>
    {
        public void Configure(EntityTypeBuilder<Hero> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Guid)
                .IsUnique();

            builder.Property(p => p.IdMarvel)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("TEXT")
                .HasMaxLength(int.MaxValue);

            builder.Property(p => p.ImageLink)
                .HasMaxLength(300);

            builder.Property(p => p.WikiLink)
                .HasMaxLength(300);

            builder.ToTable("hero");
        }
    }
}