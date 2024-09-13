using Epizon.Data;
using Epizon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ShopController : Controller
{
    private readonly EpizonContext _context;

    public ShopController(EpizonContext context)
    {
        _context = context;
    }

    // Azione per visualizzare il dettaglio dell'articolo
    public async Task<IActionResult> DettaglioArticolo(int id)
    {
        // Recupera l'articolo corrente con il rivenditore
        var articolo = await _context.Articoli
            .Include(a => a.Rivenditore)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (articolo == null)
        {
            return NotFound();
        }

        // Recupera articoli correlati appartenenti alla stessa categoria, escludendo l'articolo corrente
        var articoliCorrelati = await _context.Articoli
            .Where(a => a.Categoria == articolo.Categoria && a.Id != id)
            .Take(9) // Limita il numero di articoli correlati a 9 (o un numero preferito)
            .ToListAsync();

        // Crea il ViewModel e popola le proprietà
        var articoloViewModel = new ArticoloViewModel
        {
            Id = articolo.Id,
            Titolo = articolo.Titolo,
            ImmagineCopertina = articolo.ImmagineCopertina,
            Prezzo = articolo.Prezzo ?? 0m,
            Descrizione = articolo.Descrizione,
            RivenditoreId = articolo.RivenditoreId,
            Rivenditore = articolo.Rivenditore != null ? new RivenditoreViewModel
            {
                RagioneSociale = articolo.Rivenditore.RagioneSociale
            } : null,
            Categoria = articolo.Categoria,
            ArticoliCorrelati = articoliCorrelati.Select(a => new ArticoloViewModel
            {
                Id = a.Id,
                Titolo = a.Titolo,
                Prezzo = a.Prezzo ?? 0m,
                ImmagineCopertina = a.ImmagineCopertina
            }).ToList()
        };

        return View(articoloViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Cerca(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return View("ArticoloNonTrovato");
        }

        var articoli = await _context.Articoli
            .Where(a => a.Titolo.Contains(query) || a.Descrizione.Contains(query))
            .ToListAsync();

        if (articoli.Any())
        {
            return View("ListaArticoli", articoli);
        }

        return View("ArticoloNonTrovato");
    }
}
