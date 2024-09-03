using Epizon.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class OrdineArticolo
{
    public int  OrdineId { get; set; }
    public Ordine? Ordine { get; set; }

    public int  ArticoloId { get; set; }
    public Articolo ? Articolo { get; set; }

    public int ? Quantità { get; set; }

}
