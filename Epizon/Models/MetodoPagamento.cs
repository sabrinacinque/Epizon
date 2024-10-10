using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Epizon.Models
{
    public class MetodoPagamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Utente")]
        public int UtenteId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Tipo { get; set; }

        [StringLength(20)]
        [Column("NumeroCartaConto")] // Colonna nel DB
        public string? NumeroCarta { get; set; }

        [StringLength(100)]
        [Column("NomeIntestatario")] // Colonna nel DB
        public string? Intestatario { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataScadenza { get; set; }

        [StringLength(4)]
        public string? CodiceSicurezza { get; set; }

        [StringLength(34)]
        public string? IBAN { get; set; }

        [StringLength(100)]
        public string? NomeBanca { get; set; }

        public virtual Utente? Utente { get; set; }
    }

}
