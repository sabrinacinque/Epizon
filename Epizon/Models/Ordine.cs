using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Epizon.Models
{
    public class Ordine
    {
        public int Id { get; set; }

        [Required]
        public DateTime DataOrdine { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Totale { get; set; }

        // Foreign key per il Compratore
        public int CompratoreId { get; set; }
        public virtual Compratore? Compratore { get; set; }

        // Foreign key per il Rivenditore (attraverso gli articoli ordinati)
        public int RivenditoreId { get; set; }
        public virtual Rivenditore? Rivenditore { get; set; }

        // Relazione con gli articoli acquistati in questo ordine
        public virtual ICollection<Articolo>? Articoli { get; set; }
    }
}
