using Epizon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epizon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class CarrelloController : Controller
{
    private readonly EpizonContext _context;
    private readonly ILogger<CarrelloController> _logger;

    public CarrelloController(EpizonContext context, ILogger<CarrelloController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Azione per visualizzare il contenuto del carrello
    public IActionResult VisualizzaCarrello()
    {
        var articoli = RecuperaCarrello();
        var totale = articoli.Sum(a => a.Prezzo);

        var carrelloViewModel = new CarrelloViewModel
        {
            Articoli = articoli,
            Totale = totale
        };

        return View(carrelloViewModel);
    }

    private List<ArticoloViewModel> RecuperaCarrello()
    {
        var carrelloJson = HttpContext.Session.GetString("Carrello");
        if (!string.IsNullOrEmpty(carrelloJson))
        {
            return JsonConvert.DeserializeObject<List<ArticoloViewModel>>(carrelloJson);
        }
        return new List<ArticoloViewModel>();
    }

    [HttpPost]
    public async Task<IActionResult> AggiungiAlCarrello(int id)
    {
        var articolo = await _context.Articoli
            .Where(a => a.Id == id)
            .Select(a => new ArticoloViewModel
            {
                Id = a.Id,
                Titolo = a.Titolo,
                Prezzo = a.Prezzo ?? 0,
                ImmagineCopertina = a.ImmagineCopertina,
                Rivenditore = new RivenditoreViewModel
                {
                    RagioneSociale = a.Rivenditore.RagioneSociale
                }
            })
            .FirstOrDefaultAsync();

        if (articolo == null)
        {
            return Json(new { success = false });
        }

        var carrello = RecuperaCarrello();
        carrello.Add(articolo);
        HttpContext.Session.SetString("Carrello", JsonConvert.SerializeObject(carrello));

        int numeroProdottiNelCarrello = carrello.Count;

        return Json(new { success = true, numeroProdottiNelCarrello });
    }

    public async Task<IActionResult> Checkout()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("LoginOrRegister", "Carrello");
        }

        var email = User.Identity.Name;
        var compratore = await _context.Compratori
                                       .FirstOrDefaultAsync(c => c.Email == email);

        if (compratore == null)
        {
            return RedirectToAction("LoginOrRegister", "Carrello");
        }

        var articoli = RecuperaCarrello();
        var totale = articoli.Sum(a => a.Prezzo);

        var carrelloViewModel = new CarrelloViewModel
        {
            Articoli = articoli,
            Totale = totale,
            Compratore = compratore
        };

        return View(carrelloViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ConfermaOrdine()
    {
        try
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var email = User.Identity.Name;
            var compratore = await _context.Compratori
                                           .FirstOrDefaultAsync(c => c.Email == email);

            if (compratore == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var articoli = RecuperaCarrello();

            if (articoli == null || !articoli.Any())
            {
                return RedirectToAction("VisualizzaCarrello");
            }

            var nuovoOrdine = new Ordine
            {
                CompratoreId = compratore.Id,
                DataOrdine = DateTime.Now,
                Totale = articoli.Sum(a => a.Prezzo * a.Quantità),
                OrdineArticoli = new List<OrdineArticolo>()
            };

            _context.Ordini.Add(nuovoOrdine);
            await _context.SaveChangesAsync();

            foreach (var articolo in articoli)
            {
                var ordineArticolo = new OrdineArticolo
                {
                    OrdineId = (int)nuovoOrdine.Id,
                    ArticoloId = articolo.Id,
                    Quantità = (int)articolo.Quantità
                };

                _context.OrdineArticoli.Add(ordineArticolo);
            }

            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Carrello");

            return RedirectToAction("OrdineConfermato", new { ordineId = nuovoOrdine.Id });
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Errore durante il salvataggio dell'ordine: {Message}", dbEx.Message);
            return View("ErroreDuranteOrdine");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore generale durante il salvataggio dell'ordine: {Message}", ex.Message);
            return View("ErroreDuranteOrdine");
        }
    }


    public async Task<IActionResult> OrdineConfermato(string ordiniIds)
    {
        if (string.IsNullOrEmpty(ordiniIds))
        {
            return RedirectToAction("VisualizzaCarrello");
        }

        var ordiniIdsList = ordiniIds.Split(',').Select(int.Parse).ToList();
        var ordini = await _context.Ordini
                                   .Include(o => o.Compratore)
                                   .Where(o => ordiniIdsList.Contains((int)o.Id))
                                   .ToListAsync();

        return View(ordini);
    }

    // Puoi aggiungere un metodo per gestire errori durante l'ordine se necessario.
    public IActionResult ErroreDuranteOrdine()
    {
        // Visualizza una vista di errore
        return View();
    }
}
