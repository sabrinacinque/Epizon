
    using System.Collections.Generic;

    namespace Epizon.Models
    {
        public class CarrelloViewModel
        {
            public int Id { get; set; }

            public List<ArticoloViewModel> Articoli { get; set; }
        public decimal Totale { get; set; }
        public int QuantitaTotale { get; set; }  // Aggiungi questa proprietà per la quantità totale

        public Compratore? Compratore { get; set; } // Aggiungi questa proprietà
      

    }
}


public class ArticoloViewModel
{
    public int Id { get; set; }
    public string Titolo { get; set; }
    public string Descrizione { get; set; }
    public decimal Prezzo { get; set; }
    public int Quantità { get; set; } // Cambiato in int
    public string ImmagineCopertina { get; set; }
    public string Immagine2 { get; set; }
    public string Immagine3 { get; set; }
    public int TempiDiSpedizione { get; set; }
    public string Categoria { get; set; }
    public int? RivenditoreId { get; set; }
    public RivenditoreViewModel Rivenditore { get; set; }
}


public class RivenditoreViewModel
    {
        public string RagioneSociale { get; set; }
    }


