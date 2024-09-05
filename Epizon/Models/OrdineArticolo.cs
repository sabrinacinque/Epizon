namespace Epizon.Models
{

    public class OrdineArticolo
    {
        internal readonly int? Id;

        public int OrdineId { get; set; }
        public Ordine? Ordine { get; set; }

        public int ArticoloId { get; set; }
        public Articolo? Articolo { get; set; }

        public int? Quantità { get; set; }



    }
}