using Epizon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epizon.Data;
using Microsoft.EntityFrameworkCore;

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
        var totale = articoli.Sum(a => a.Prezzo); // Calcola il totale del carrello

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
            // Reindirizza alla pagina di accesso e registrazione se l'utente non è autenticato
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




    [HttpGet]
    public IActionResult LoginOrRegister()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ConfermaOrdine()
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
        if (articoli.Count == 0)
        {
            return RedirectToAction("VisualizzaCarrello");
        }

        var ordine = new Ordine
        {
            CompratoreId = compratore.Id,
            DataOrdine = DateTime.Now,
            Totale = articoli.Sum(a => a.Prezzo)
        };

        _context.Ordini.Add(ordine);

        foreach (var articolo in articoli)
        {
            _context.OrdineArticoli.Add(new OrdineArticolo
            {
                OrdineId = ordine.Id,
                ArticoloId = articolo.Id,
                Quantità = 1 // Usa la quantità appropriata se disponibile
            });
        }

        await _context.SaveChangesAsync();

        // Rimuovere gli articoli dal carrello
        HttpContext.Session.Remove("Carrello");

        // Reindirizzare alla pagina di conferma
        return RedirectToAction("ConfermaOrdine");
    }




}
