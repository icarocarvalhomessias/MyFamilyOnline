using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FML.Familiares.API.Data.Mappings
{
    public class FamilyMapping : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(f => f.StartDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(f => f.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(f => f.History)
                .HasColumnType("varchar(1000)");

            builder.HasMany(f => f.Relatives)
                .WithOne(r => r.Family)
                .HasForeignKey(r => r.FamilyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(f => f.Houses)
                .WithOne(h => h.Family)
                .HasForeignKey(h => h.FamilyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Families");
        }
    }
}
