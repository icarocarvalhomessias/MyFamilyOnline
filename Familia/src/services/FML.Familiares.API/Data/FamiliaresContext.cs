using FML.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace FML.Familiares.API.Data
{
    public class FamiliaresContext : DbContext, IUnitOfWork
    {
        public FamiliaresContext(DbContextOptions<FamiliaresContext> options) : base(options)
        {
        }

        public DbSet<Relative> Relatives { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<House> Houses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Family-Relative relationship
            modelBuilder.Entity<Family>()
                .HasMany(f => f.Relatives)
                .WithOne(r => r.Family)
                .HasForeignKey(r => r.FamilyId);

            // Configure House-Relative relationship
            modelBuilder.Entity<House>()
                .HasMany(h => h.Residents)
                .WithOne(r => r.House)
                .HasForeignKey(r => r.HouseId);

            // Configure Relative-Spouse relationship
            modelBuilder.Entity<Relative>()
                .HasOne(r => r.SpouseObj)
                .WithOne()
                .HasForeignKey<Relative>(r => r.Spouse);

            // Configure Relative-Children relationship
            modelBuilder.Entity<Relative>()
                .HasMany(r => r.Children)
                .WithOne()
                .HasForeignKey(r => r.FatherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relative>()
                .HasMany(r => r.Children)
                .WithOne()
                .HasForeignKey(r => r.MotherId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
