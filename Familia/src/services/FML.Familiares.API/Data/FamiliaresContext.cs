using FML.Core.Data;
using FML.Core.DomainObjects;
using FML.Core.Mediator;
using FML.Core.Messages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FML.Familiares.API.Data
{
    public class FamiliaresContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public FamiliaresContext(DbContextOptions<FamiliaresContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Relative> Relatives { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<House> Houses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

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
            var sucesso = await base.SaveChangesAsync() > 0;

            if (sucesso) await _mediatorHandler.PublicarEventos(this);

            return sucesso;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T context) where T : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublicarEvento(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
