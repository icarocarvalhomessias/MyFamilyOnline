using FML.Core.Data;
using FML.Familiares.API.Data.Mappings;
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
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfiguration(new RelativeMapping());
            modelBuilder.ApplyConfiguration(new HouseMapping());
            modelBuilder.ApplyConfiguration(new FamilyMapping());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
