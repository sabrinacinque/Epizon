namespace Epizon.Models
{
    public class CarrelloViewModel
    {
        public List<ArticoloViewModel> Articoli { get; set; } = new List<ArticoloViewModel>();
        public int QuantitàArticoli => (int)Articoli.Sum(a => a.Quantità);
    }

    public class ArticoloViewModel
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public decimal Quantità { get; set; }
        public string ImmagineCopertina { get; set; }
        public string Immagine2 { get; set; }
        public string Immagine3 { get; set; }
        public int TempiDiSpedizione { get; set; }
        public string Categoria { get; set; }
        public int? RivenditoreId { get; set; }
        public RivenditoreViewModel Rivenditore { get; set; } // Aggiungi questa proprietà
    }

    public class RivenditoreViewModel
    {
        public string RagioneSociale { get; set; }
    }

}
