namespace Epizon.Models
{
    public class OrdineDettagliViewModel
    {
        public int Id { get; set; }
        public DateTime DataOrdine { get; set; }
        public decimal Totale { get; set; }
        public List<ArticoloDettagliViewModel> Articoli { get; set; }
    }

    public class ArticoloDettagliViewModel
    {
        public string Titolo { get; set; }
        public decimal Prezzo { get; set; }
        public int Quantità { get; set; }
    }

}
