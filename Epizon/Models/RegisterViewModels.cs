using System.ComponentModel.DataAnnotations;

namespace Epizon.Models
{
    public class RegisterCompratoreViewModel
    {
        
        
        [Display(Name = "Email")]
        public string? Email { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string ? Password { get; set; }

        
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
    }

    public class RegisterRivenditoreViewModel
    {
        
        
        public string ? Email { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        
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
    }
}
