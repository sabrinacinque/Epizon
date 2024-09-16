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
        var totale = articoli.Sum(a => a.Prezzo * a.Quantità);

        var carrelloViewModel = new CarrelloViewModel
        {
            Articoli = articoli,
            Totale = totale
        };

        return View(carrelloViewModel);
    }

    public IActionResult LoginOrRegister()
    {
        return View();
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
                },
                Quantità = 1 // Imposta la quantità di default a 1
            })
            .FirstOrDefaultAsync();

        if (articolo == null)
        {
            return Json(new { success = false });
        }

        var carrello = RecuperaCarrello();

        // Controlla se l'articolo è già nel carrello
        var articoloEsistente = carrello.FirstOrDefault(a => a.Id == id);
        if (articoloEsistente != null)
        {
            // Incrementa la quantità
            articoloEsistente.Quantità += 1;
        }
        else
        {
            // Aggiungi nuovo articolo al carrello
            carrello.Add(articolo);
        }

        HttpContext.Session.SetString("Carrello", JsonConvert.SerializeObject(carrello));

        int numeroProdottiNelCarrello = carrello.Sum(a => a.Quantità); // Conta la quantità totale

        return Json(new { success = true, numeroProdottiNelCarrello });
    }

    [HttpPost]
    public IActionResult AggiornaQuantita(List<ArticoloViewModel> articoli)
    {
        var carrello = RecuperaCarrello();

        // Aggiorna le quantità degli articoli nel carrello
        foreach (var articolo in articoli)
        {
            var articoloEsistente = carrello.FirstOrDefault(a => a.Id == articolo.Id);
            if (articoloEsistente != null)
            {
                articoloEsistente.Quantità = articolo.Quantità;
            }
        }

        // Calcola il totale aggiornato
        var totale = carrello.Sum(a => a.Prezzo * a.Quantità);

        // Salva di nuovo il carrello nella sessione
        HttpContext.Session.SetString("Carrello", JsonConvert.SerializeObject(carrello));

        // Redirect alla pagina di checkout
        return RedirectToAction("Checkout");
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        // Recupera il carrello dalla sessione
        var carrelloJson = HttpContext.Session.GetString("Carrello");
        var carrelloViewModel = JsonConvert.DeserializeObject<CarrelloViewModel>(carrelloJson);

        // Assicurati che il compratore sia impostato
        if (carrelloViewModel.Compratore == null)
        {
            // Potresti voler redirigere a una pagina di errore o impostare un compratore predefinito
            return RedirectToAction("Error", "Home");
        }

        return View(carrelloViewModel);
    }

    private async Task<Compratore> RecuperaCompratore()
    {
        // Verifica che l'utente sia autenticato
        if (!User.Identity.IsAuthenticated)
        {
            return null; // Se l'utente non è autenticato, restituisci null
        }

        // Ottieni l'email dell'utente autenticato
        var email = User.Identity.Name;

        // Cerca il compratore nel database utilizzando l'email
        var compratore = await _context.Compratori.FirstOrDefaultAsync(c => c.Email == email);

        return compratore; // Restituisci il compratore o null se non trovato
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(Dictionary<int, int> Quantita)
    {
        // Controlla se l'utente è autenticato
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("LoginOrRegister", "Carrello");
        }

        var articoli = RecuperaCarrello();  // Recupera gli articoli dal carrello

        if (articoli == null || !articoli.Any())
        {
            ModelState.AddModelError("", "Il carrello è vuoto.");
            return RedirectToAction("VisualizzaCarrello");
        }

        // Aggiorna le quantità degli articoli
        foreach (var articolo in articoli)
        {
            if (Quantita.ContainsKey(articolo.Id))
            {
                articolo.Quantità = Quantita[articolo.Id];
            }
        }

        // Calcola il totale
        var totale = articoli.Sum(a => a.Prezzo * a.Quantità);

        // Recupera i dettagli del compratore
        var compratore = await RecuperaCompratore();
        if (compratore == null)
        {
            ModelState.AddModelError("", "Compratore non trovato.");
            return RedirectToAction("Login", "Account");
        }

        var carrelloViewModel = new CarrelloViewModel
        {
            Articoli = articoli,
            Totale = totale,
            Compratore = compratore
        };

        return View("Checkout", carrelloViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ConfermaOrdine()
    {
        // Ottieni gli articoli dal carrello
        var carrelloArticoli = RecuperaCarrello();

        if (carrelloArticoli == null || !carrelloArticoli.Any())
        {
            // Nessun articolo nel carrello
            return RedirectToAction("VisualizzaCarrello");
        }

        // Recupera i dettagli del compratore autenticato
        var compratore = await RecuperaCompratore();
        if (compratore == null)
        {
            ModelState.AddModelError("", "Compratore non trovato.");
            return RedirectToAction("Login", "Account");
        }

        // Crea un nuovo ordine
        var ordine = new Ordine
        {
            DataOrdine = DateTime.Now,
            Totale = carrelloArticoli.Sum(a => a.Prezzo * a.Quantità),
            CompratoreId = compratore.Id, // Associa l'ID del compratore all'ordine
            OrdineArticoli = carrelloArticoli.Select(a => new OrdineArticolo
            {
                ArticoloId = a.Id,
                Quantità = a.Quantità,
                Prezzo = a.Prezzo
            }).ToList()
        };

        // Salva l'ordine nel database
        _context.Ordini.Add(ordine);
        await _context.SaveChangesAsync();

        // Pulisci il carrello
        HttpContext.Session.Remove("Carrello");

        return View("OrdineConfermato", new List<Ordine> { ordine });
    }


    public async Task<IActionResult> OrdineConfermato(int ordiniIds)
    {
        if (ordiniIds == 0)
        {
            return RedirectToAction("VisualizzaCarrello");
        }

        // Recupera l'ordine dal database usando l'ID
        var ordine = await _context.Ordini
                                   .Include(o => o.OrdineArticoli)
                                   .ThenInclude(oa => oa.Articolo)
                                   .FirstOrDefaultAsync(o => o.Id == ordiniIds);

        if (ordine == null)
        {
            return NotFound();
        }

        var model = new List<Ordine>
        {
            ordine
        };

        return View(model);
    }

    // Puoi aggiungere un metodo per gestire errori durante l'ordine se necessario.
    public IActionResult ErroreDuranteOrdine()
    {
        // Visualizza una vista di errore
        return View();
    }

    public async Task<IActionResult> ImieiOrdini()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var email = User.Identity.Name;
        var compratore = await _context.Compratori.FirstOrDefaultAsync(c => c.Email == email);

        if (compratore == null)
        {
            return RedirectToAction("Errore", "Home");
        }

        var ordini = await _context.Ordini
            .Where(o => o.CompratoreId == compratore.Id)
            .Include(o => o.OrdineArticoli)
            .ThenInclude(oa => oa.Articolo)
            .OrderByDescending(o => o.DataOrdine)  // Ordina per data in ordine decrescente
            .ToListAsync();

        return View(ordini);
    }



    public async Task<IActionResult> DettagliOrdine(int id)
    {
        var ordine = await _context.Ordini
            .Include(o => o.OrdineArticoli)
            .ThenInclude(oa => oa.Articolo)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (ordine == null)
        {
            return NotFound();
        }

        // Verifica se le proprietà nullable hanno un valore prima di usarle
        var model = new OrdineDettagliViewModel
        {
            Id = (int)ordine.Id, // Non è nullable, quindi il cast non è necessario
            DataOrdine = ordine.DataOrdine.HasValue ? ordine.DataOrdine.Value : DateTime.MinValue,
            Totale = ordine.Totale.HasValue ? ordine.Totale.Value : 0m,
            Articoli = ordine.OrdineArticoli.Select(oa => new ArticoloDettagliViewModel
            {
                Titolo = oa.Articolo.Titolo,
                Prezzo = oa.Prezzo.HasValue ? oa.Prezzo.Value : 0m,
                Quantità = oa.Quantità.HasValue ? oa.Quantità.Value : 0
            }).ToList()
        };

        return View(model);
    }



    // Metodo per rimuovere un articolo dal carrello
    [HttpPost]
    public IActionResult RimuoviArticolo(int id)
    {
        var carrello = RecuperaCarrello();

        var articolo = carrello.FirstOrDefault(a => a.Id == id);
        if (articolo != null)
        {
            carrello.Remove(articolo);
            HttpContext.Session.SetString("Carrello", JsonConvert.SerializeObject(carrello));
        }

        return RedirectToAction("VisualizzaCarrello");
    }

    // Metodo per svuotare il carrello
    [HttpPost]
    public IActionResult SvuotaCarrello()
    {
        HttpContext.Session.Remove("Carrello");
        return RedirectToAction("VisualizzaCarrello");
    }
}
