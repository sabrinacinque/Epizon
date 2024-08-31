using Epizon.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class OrdineArticolo
{
    public int OrdineId { get; set; }
    public Ordine? Ordine { get; set; }

    public int ArticoloId { get; set; }
    public Articolo? Articolo { get; set; }

    public int Quantità { get; set; }

    // Chiave primaria composta
    public class OrdineArticoloConfiguration : IEntityTypeConfiguration<OrdineArticolo>
    {
        public void Configure(EntityTypeBuilder<OrdineArticolo> builder)
        {
            builder.HasKey(oa => new { oa.OrdineId, oa.ArticoloId });
        }
    }
}
