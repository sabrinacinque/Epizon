using Microsoft.EntityFrameworkCore;
using Epizon.Models;

namespace Epizon.Data
{
    public class EpizonContext : DbContext
    {
        public EpizonContext(DbContextOptions<EpizonContext> options) : base(options)
        {
        }

        public DbSet<Articolo> Articoli { get; set; }
        public DbSet<Ordine> Ordini { get; set; }
        public DbSet<Compratore> Compratori { get; set; }
        public DbSet<Rivenditore> Rivenditori { get; set; }
        public DbSet<OrdineArticolo> OrdineArticoli { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Utente>()
                .HasDiscriminator<string>("Ruolo")
                .HasValue<Compratore>("Compratore")
                .HasValue<Rivenditore>("Rivenditore");

            modelBuilder.Entity<Ordine>()
                .HasOne(o => o.Compratore)
                .WithMany(c => c.OrdiniEffettuati)
                .HasForeignKey(o => o.CompratoreId);

            modelBuilder.Entity<Ordine>()
                .HasOne(o => o.Rivenditore)
                .WithMany(r => r.OrdiniRicevuti)
                .HasForeignKey(o => o.RivenditoreId);

            modelBuilder.Entity<OrdineArticolo>()
                .HasKey(oa => new { oa.OrdineId, oa.ArticoloId });
        }
    }
}
