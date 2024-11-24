using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FML.Familiares.API.Data.Mappings
{
    public class HouseMapping : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(h => h.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(h => h.FamilyId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(h => h.Address)
                .HasColumnType("varchar(200)");

            builder.Property(h => h.City)
                .HasColumnType("varchar(100)");

            builder.Property(h => h.State)
                .HasColumnType("varchar(50)");

            builder.Property(h => h.ZipCode)
                .HasColumnType("varchar(20)");

            builder.HasOne(h => h.Family)
                .WithMany(f => f.Houses)
                .HasForeignKey(h => h.FamilyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Houses");
        }
    }
}
