using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Epizon.Models
{
    public class Rivenditore : Utente
    {
       
        [StringLength(200)]
        public string? RagioneSociale { get; set; }

        
        [StringLength(100)]
        public string? Nome { get; set; }

        
        [StringLength(100)]
        public string? Cognome { get; set; }

        
        [StringLength(11)]
        public string? PartitaIva { get; set; }

        
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


        
        [StringLength(200)]
        public string? Pec { get; set; }

        [StringLength(7)]
        public string? CodiceDestinatario { get; set; }

        // Relazione con gli articoli del rivenditore
        public virtual ICollection<Articolo>? Articoli { get; set; }

        // Relazione con gli ordini ricevuti
        public virtual ICollection<Ordine>? OrdiniRicevuti { get; set; }
    }
}
