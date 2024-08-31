using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Epizon.Data;
using Epizon.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Epizon.Controllers
{
    public class ArticoliController : Controller
    {
        private readonly EpizonContext _context;

        public ArticoliController(EpizonContext context)
        {
            _context = context;
        }

        // GET: Articoli
        public async Task<IActionResult> Index()
        {
            // Recupera l'ID del rivenditore dai claims dell'utente loggato
            var rivenditoreIdClaim = User.Claims.FirstOrDefault(c => c.Type == "RivenditoreId");
            if (rivenditoreIdClaim == null)
            {
                return Unauthorized();
            }

            var rivenditoreId = int.Parse(rivenditoreIdClaim.Value);

            // Filtra gli articoli per il rivenditore loggato
            var articoli = _context.Articoli
                .Include(a => a.Ordine)
                .Include(a => a.Rivenditore)
                .Where(a => a.RivenditoreId == rivenditoreId);

            return View(await articoli.ToListAsync());
        }

        // GET: Articoli/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articolo = await _context.Articoli
                .Include(a => a.Ordine)
                .Include(a => a.Rivenditore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articolo == null)
            {
                return NotFound();
            }

            return View(articolo);
        }

        // GET: Articoli/Create
        public IActionResult Create()
        {
            ViewData["OrdineId"] = new SelectList(_context.Ordini, "Id", "Id");
            ViewData["RivenditoreId"] = new SelectList(_context.Rivenditori, "Id", "Email");

            // Aggiunta delle categorie predefinite
            ViewData["Categorie"] = new List<SelectListItem>
            {
                new SelectListItem { Value = "Categoria1", Text = "Categoria1" },
                new SelectListItem { Value = "Categoria2", Text = "Categoria2" },
                new SelectListItem { Value = "Categoria3", Text = "Categoria3" }
            };

            return View();
        }

        // POST: Articoli/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titolo,Descrizione,Prezzo,TempiDiSpedizione,Categoria")] Articolo articolo, IFormFile ImmagineCopertina, IFormFile Immagine2, IFormFile Immagine3)
        {
            if (ModelState.IsValid)
            {
                // Recupera il RivenditoreId dai claims dell'utente loggato
                var rivenditoreIdClaim = User.Claims.FirstOrDefault(c => c.Type == "RivenditoreId");
                if (rivenditoreIdClaim != null)
                {
                    articolo.RivenditoreId = int.Parse(rivenditoreIdClaim.Value);
                }
                else
                {
                    // Se per qualche motivo il RivenditoreId non è presente nei claims, gestisci l'errore
                    return Unauthorized();
                }

                // Salvataggio delle immagini
                articolo.ImmagineCopertina = await SalvaImmagine(ImmagineCopertina);
                articolo.Immagine2 = await SalvaImmagine(Immagine2);
                articolo.Immagine3 = await SalvaImmagine(Immagine3);

                _context.Add(articolo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Ripopolare le categorie in caso di errore
            ViewData["Categorie"] = new List<SelectListItem>
    {
        new SelectListItem { Value = "Categoria1", Text = "Categoria1" },
        new SelectListItem { Value = "Categoria2", Text = "Categoria2" },
        new SelectListItem { Value = "Categoria3", Text = "Categoria3" }
    };

            return View(articolo);
        }


        // GET: Articoli/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articolo = await _context.Articoli.FindAsync(id);
            if (articolo == null)
            {
                return NotFound();
            }

            ViewData["OrdineId"] = new SelectList(_context.Ordini, "Id", "Id", articolo.OrdineId);
            ViewData["RivenditoreId"] = new SelectList(_context.Rivenditori, "Id", "Email", articolo.RivenditoreId);

            // Aggiunta delle categorie predefinite
            ViewData["Categorie"] = new List<SelectListItem>
            {
                new SelectListItem { Value = "Categoria1", Text = "Categoria1" },
                new SelectListItem { Value = "Categoria2", Text = "Categoria2" },
                new SelectListItem { Value = "Categoria3", Text = "Categoria3" }
            };

            return View(articolo);
        }

        // POST: Articoli/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titolo,Descrizione,Prezzo,TempiDiSpedizione,Categoria,OrdineId")] Articolo articolo, IFormFile ImmagineCopertina, IFormFile Immagine2, IFormFile Immagine3)
        {
            if (id != articolo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Salvataggio delle immagini se presenti
                    articolo.ImmagineCopertina = ImmagineCopertina != null ? await SalvaImmagine(ImmagineCopertina) : articolo.ImmagineCopertina;
                    articolo.Immagine2 = Immagine2 != null ? await SalvaImmagine(Immagine2) : articolo.Immagine2;
                    articolo.Immagine3 = Immagine3 != null ? await SalvaImmagine(Immagine3) : articolo.Immagine3;

                    _context.Update(articolo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticoloExists(articolo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["OrdineId"] = new SelectList(_context.Ordini, "Id", "Id", articolo.OrdineId);
            ViewData["RivenditoreId"] = new SelectList(_context.Rivenditori, "Id", "Email", articolo.RivenditoreId);

            // Ripopolare le categorie in caso di errore
            ViewData["Categorie"] = new List<SelectListItem>
            {
                new SelectListItem { Value = "Categoria1", Text = "Categoria1" },
                new SelectListItem { Value = "Categoria2", Text = "Categoria2" },
                new SelectListItem { Value = "Categoria3", Text = "Categoria3" }
            };

            return View(articolo);
        }

        // GET: Articoli/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articolo = await _context.Articoli
                .Include(a => a.Ordine)
                .Include(a => a.Rivenditore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articolo == null)
            {
                return NotFound();
            }

            return View(articolo);
        }

        // POST: Articoli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articolo = await _context.Articoli.FindAsync(id);
            if (articolo != null)
            {
                _context.Articoli.Remove(articolo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticoloExists(int id)
        {
            return _context.Articoli.Any(e => e.Id == id);
        }

        // Funzione per salvare le immagini
        private async Task<string> SalvaImmagine(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return file.FileName;
        }
    }
}
