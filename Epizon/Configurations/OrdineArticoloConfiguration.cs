using Epizon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrdineArticoloConfiguration : IEntityTypeConfiguration<OrdineArticolo>
{
    public void Configure(EntityTypeBuilder<OrdineArticolo> builder)
    {
        builder.HasKey(oa => new { oa.OrdineId, oa.ArticoloId });
    }
}
