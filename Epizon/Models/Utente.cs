using Epizon.Models;
using System.ComponentModel.DataAnnotations;

public abstract class Utente
{
    public int Id { get; set; }


    public string? Email { get; set; }



    public string? Password { get; set; }

    // Ruolo dell'utente, usato per differenziare tra Compratore e Rivenditore
    public string? Ruolo { get; set; }

    public virtual ICollection<MetodoPagamento>? MetodiPagamento { get; set; }



}
