using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Epizon.Models
{
    public class Articolo
    {
        public int Id { get; set; }

        
        [StringLength(200)]
        public string? Titolo { get; set; }

        
        [StringLength(1000)]
        public string? Descrizione { get; set; }

        public decimal? Prezzo { get; set; }

        
        public string? ImmagineCopertina { get; set; }

        public string? Immagine2 { get; set; }
        public string? Immagine3 { get; set; }

        
        public int? TempiDiSpedizione { get; set; }

        [StringLength(100)]
        public string? Categoria { get; set; }

        // Foreign key per il rivenditore
        public int? RivenditoreId { get; set; }
        public virtual Rivenditore? Rivenditore { get; set; }
     
    }
}
