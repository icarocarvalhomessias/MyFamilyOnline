using FluentValidation.Results;
using FML.Core.Data;
using FML.Core.Messages;
using FML.Evento.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FML.Evento.API.Data
{
    public class EventoContext : DbContext, IUnitOfWork
    {
        public EventoContext(DbContextOptions<EventoContext> options) : base(options)
        {
        }

        public DbSet<EventoFamilia> Eventos { get; set; }
        public DbSet<ListaDeDesejos> ListasDeDesejos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            base.OnModelCreating(modelBuilder);

            // Configuring EventoFamilia entity
            modelBuilder.Entity<EventoFamilia>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Theme).HasMaxLength(100);
            });

            // Configuring ListaDeDesejos entity
            modelBuilder.Entity<ListaDeDesejos>(entity =>
            {
                entity.HasKey(ld => ld.Id);
                entity.Property(ld => ld.Nome).IsRequired().HasMaxLength(100);
                entity.Property(ld => ld.Descricao).HasMaxLength(500);
                entity.Property(ld => ld.Link).HasMaxLength(200);
                entity.Property(ld => ld.Preco).HasMaxLength(50);
                entity.Property(ld => ld.Loja).HasMaxLength(100);
                entity.Property(ld => ld.Observacao).HasMaxLength(500);
            });
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
