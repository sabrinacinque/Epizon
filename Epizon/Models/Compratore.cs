using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Epizon.Models
{
    public class Compratore : Utente
    {
        
        [StringLength(100)]
        public string? Nome { get; set; }

        
        [StringLength(100)]
        public string? Cognome { get; set; }

        
        [StringLength(200)]
        public string? Indirizzo { get; set; }

        
        [StringLength(100)]
        public string? Citta { get; set; }

        
        [StringLength(5)]
        public string? CAP { get; set; }

        
        [StringLength(100)]
        public string? Provincia { get; set; }

       
        [StringLength(15)]
        public string? Telefono { get; set; }

        // Relazione con gli ordini effettuati
        public virtual ICollection<Ordine>? OrdiniEffettuati { get; set; }
        public virtual ICollection<MetodoPagamento>? MetodiPagamento { get; set; }


    }
}
