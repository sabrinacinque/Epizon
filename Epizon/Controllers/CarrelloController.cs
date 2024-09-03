using Epizon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CarrelloController : Controller
{
    private readonly ILogger<CarrelloController> _logger;

    public CarrelloController(ILogger<CarrelloController> logger)
    {
        _logger = logger;
    }

    // Azione per visualizzare il contenuto del carrello
    public IActionResult VisualizzaCarrello()
    {
        var carrello = RecuperaCarrello();
        return View(carrello);
    }

    private List<ArticoloViewModel> RecuperaCarrello()
    {
        var carrelloJson = HttpContext.Session.GetString("Carrello");
        if (carrelloJson != null)
        {
            return JsonConvert.DeserializeObject<List<ArticoloViewModel>>(carrelloJson);
        }
        return new List<ArticoloViewModel>();
    }

    [HttpPost]
    public IActionResult AggiungiAlCarrello(int id)
    {
        var articolo = RecuperaArticoloDaId(id);
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

    private ArticoloViewModel RecuperaArticoloDaId(int id)
    {
        // Simula il recupero dell'articolo
        return new ArticoloViewModel
        {
            Id = id,
            Titolo = "Articolo " + id,
            Prezzo = 19.99m,
            ImmagineCopertina = "immagine.jpg"
        };
    }
}
