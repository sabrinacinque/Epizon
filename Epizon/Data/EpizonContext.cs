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

            // Configurazione del discriminatore per Utente
            modelBuilder.Entity<Utente>()
                .HasDiscriminator<string>("Ruolo")
                .HasValue<Compratore>("Compratore")
                .HasValue<Rivenditore>("Rivenditore");

            // Configurazione della relazione tra Ordine e Compratore
            modelBuilder.Entity<Ordine>()
                .HasOne(o => o.Compratore)
                .WithMany(c => c.OrdiniEffettuati)
                .HasForeignKey(o => o.CompratoreId);

            // Configurazione della chiave primaria composta per OrdineArticolo
            modelBuilder.Entity<OrdineArticolo>()
                .HasKey(oa => new { oa.OrdineId, oa.ArticoloId });

            // Configurazione della relazione tra Articolo e Rivenditore
            modelBuilder.Entity<Articolo>()
                .HasOne(a => a.Rivenditore)
                .WithMany(r => r.Articoli)
                .HasForeignKey(a => a.RivenditoreId)
                .OnDelete(DeleteBehavior.Restrict); // Imposta DeleteBehavior se necessario
        }
    }
}
