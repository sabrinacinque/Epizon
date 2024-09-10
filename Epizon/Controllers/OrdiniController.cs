using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Epizon.Data;
using Epizon.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

//[Authorize(Roles = "Rivenditore")]
public class OrdiniController : Controller
{
    private readonly EpizonContext _context;

    public OrdiniController(EpizonContext context)
    {
        _context = context;
    }

    // GET: Ordini/OrdiniRicevuti
    public async Task<IActionResult> OrdiniRicevuti()
    {
        var rivenditoreIdClaim = User.Claims.FirstOrDefault(c => c.Type == "RivenditoreId");
        if (rivenditoreIdClaim == null)
        {
            return Unauthorized();
        }

        var rivenditoreId = int.Parse(rivenditoreIdClaim.Value);

        var ordiniRicevuti = await _context.OrdineArticoli
            .Include(oa => oa.Ordine)
                .ThenInclude(o => o.Compratore)
            .Include(oa => oa.Articolo)
            .Where(oa => oa.Articolo.RivenditoreId == rivenditoreId)
            .Select(oa => new OrdineRicevutoViewModel
            {
                OrdineId = (int)oa.Ordine.Id, // Assicurati di usare la proprietà giusta qui
                DataOrdine = oa.Ordine.DataOrdine,
                ArticoloTitolo = oa.Articolo.Titolo,
                ArticoloDescrizione = oa.Articolo.Descrizione,
                ArticoloImmagineCopertina = oa.Articolo.ImmagineCopertina,
                ArticoloPrezzo = oa.Articolo.Prezzo,
                QuantitàOrdinata = oa.Quantità,
                NomeCompratore = oa.Ordine.Compratore.Nome,
                CognomeCompratore = oa.Ordine.Compratore.Cognome,
                IndirizzoCompratore = oa.Ordine.Compratore.Indirizzo,
                CittàCompratore = oa.Ordine.Compratore.Citta,
                CAPCompratore = oa.Ordine.Compratore.CAP,
                ProvinciaCompratore = oa.Ordine.Compratore.Provincia,
                TelefonoCompratore = oa.Ordine.Compratore.Telefono
            })
            .OrderByDescending(o => o.DataOrdine)
            .ToListAsync();

        return View(ordiniRicevuti);
    }


    // GET: Ordini/DettagliOrdine/5
    public async Task<IActionResult> DettagliOrdine(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        int rivenditoreId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "RivenditoreId")?.Value);

        var ordineArticolo = await _context.OrdineArticoli
            .Include(oa => oa.Ordine)
                .ThenInclude(o => o.Compratore)
            .Include(oa => oa.Articolo)
            .Where(oa => oa.OrdineId == id && oa.Articolo.RivenditoreId == rivenditoreId)
            .FirstOrDefaultAsync();

        if (ordineArticolo == null)
        {
            return NotFound();
        }

        return View(ordineArticolo);
    }



    // GET: Ordini/OrdiniRicevutiOggi
    [HttpGet]
    public async Task<IActionResult> OrdiniRicevutiOggi()
    {
        var rivenditoreIdClaim = User.Claims.FirstOrDefault(c => c.Type == "RivenditoreId");
        if (rivenditoreIdClaim == null)
        {
            return Unauthorized();
        }

        var rivenditoreId = int.Parse(rivenditoreIdClaim.Value);
        var oggi = DateTime.Today;

        var ordiniOggi = await _context.OrdineArticoli
            .Where(oa => oa.Articolo.RivenditoreId == rivenditoreId && oa.Ordine.DataOrdine >= oggi)
            .CountAsync(); // Conteggia gli ordini ricevuti oggi

        return Json(ordiniOggi);
    }

    // GET: Ordini/OrdiniRicevutiUltimaSettimana
    [HttpGet]
    public async Task<IActionResult> OrdiniRicevutiUltimaSettimana()
    {
        var rivenditoreIdClaim = User.Claims.FirstOrDefault(c => c.Type == "RivenditoreId");
        if (rivenditoreIdClaim == null)
        {
            return Unauthorized();
        }

        var rivenditoreId = int.Parse(rivenditoreIdClaim.Value);
        var ultimaSettimana = DateTime.Today.AddDays(-7);

        var ordiniSettimana = await _context.OrdineArticoli
            .Where(oa => oa.Articolo.RivenditoreId == rivenditoreId && oa.Ordine.DataOrdine >= ultimaSettimana)
            .CountAsync(); // Conteggia gli ordini ricevuti nell'ultima settimana

        return Json(ordiniSettimana);
    }

}
