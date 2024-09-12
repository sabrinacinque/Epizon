using Epizon.Data;
using Epizon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ShopController : Controller
{
    private readonly EpizonContext _context; // Il contesto del database

    public ShopController(EpizonContext context)
    {
        _context = context;
    }

    // Azione per visualizzare il dettaglio dell'articolo
    public async Task<IActionResult> DettaglioArticolo(int id)
    {
        var articolo = await _context.Articoli
            .FirstOrDefaultAsync(a => a.Id == id);

        if (articolo == null)
        {
            return NotFound();
        }

        // Crea il ViewModel e popola le proprietà
        var articoloViewModel = new ArticoloViewModel
        {
            Id = articolo.Id,
            Titolo = articolo.Titolo,
            ImmagineCopertina = articolo.ImmagineCopertina,
            Prezzo = articolo.Prezzo ?? 0m, // Assicurati che Prezzo non sia null
            Descrizione = articolo.Descrizione,
            RivenditoreId = articolo.RivenditoreId,
            Rivenditore = articolo.Rivenditore != null ? new RivenditoreViewModel
            {
                RagioneSociale = articolo.Rivenditore.RagioneSociale
            } : null
        };

        return View(articoloViewModel);
    }



    [HttpGet]
    public async Task<IActionResult> Cerca(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return View("ArticoloNonTrovato"); // Se la query è vuota, mostra un messaggio
        }

        // Cerca articoli che contengano la query nel titolo o nella descrizione
        var articoli = await _context.Articoli
            .Where(a => a.Titolo.Contains(query) || a.Descrizione.Contains(query))
            .ToListAsync();

        if (articoli.Any())
        {
            // Restituisce una vista con la lista di articoli trovati
            return View("ListaArticoli", articoli);
        }

        // Se nessun articolo è trovato, mostra una pagina di errore
        return View("ArticoloNonTrovato");
    }



}
