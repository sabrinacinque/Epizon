namespace Epizon.Models
{
    // Models/OrdineRicevutoViewModel.cs
    public class OrdineRicevutoViewModel
    {
        public DateTime? DataOrdine { get; set; }
        public string ArticoloTitolo { get; set; }
        public string ArticoloDescrizione { get; set; }
        public string ArticoloImmagineCopertina { get; set; }
        public decimal? ArticoloPrezzo { get; set; }
        public int? QuantitàOrdinata { get; set; }
        public string NomeCompratore { get; set; }
        public string CognomeCompratore { get; set; }
        public string IndirizzoCompratore { get; set; }
        public string CittàCompratore { get; set; }
        public string CAPCompratore { get; set; }
        public string ProvinciaCompratore { get; set; }
        public string TelefonoCompratore { get; set; }
    }

}
