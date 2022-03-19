using MarvelHeroes.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarvelHeroes.Data.Mappings
{
    public class HqMapping : IEntityTypeConfiguration<Hq>
    {
        public void Configure(EntityTypeBuilder<Hq> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(p => p.Guid)
                   .IsUnique();

            builder.Property(c => c.IdMarvel)
                .IsRequired();

            builder.Property(c => c.Title)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Description)
                .HasColumnType("TEXT")
                .HasMaxLength(int.MaxValue);

            builder.Property(c => c.ImageLink)
                .HasMaxLength(300);

            builder.Property(c => c.WikiLink)
                .HasMaxLength(300);

            builder.ToTable("hq");
        }
    }
}