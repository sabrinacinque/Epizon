using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Epizon.Data;
using Epizon.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

public class RivenditoreController : Controller
{
    private readonly EpizonContext _context;

    public RivenditoreController(EpizonContext context)
    {
        _context = context;
    }

    // GET: Rivenditore/Profilo
    public async Task<IActionResult> Profilo()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("LoginRivenditore", "Account"); // Redirect to login if not authenticated
        }

        var email = User.Identity.Name;
        var rivenditore = await _context.Rivenditori.FirstOrDefaultAsync(r => r.Email == email);

        if (rivenditore == null)
        {
            return NotFound();
        }

        return View(rivenditore);
    }

    // GET: Rivenditore/ModificaProfilo
    public async Task<IActionResult> ModificaProfilo()
    {
        var email = User.Identity.Name;
        if (email == null)
        {
            return RedirectToAction("LoginRivenditore", "Account");
        }

        var rivenditore = await _context.Rivenditori.FirstOrDefaultAsync(r => r.Email == email);

        if (rivenditore == null)
        {
            return NotFound();
        }

        return View(rivenditore);
    }

    // POST: Rivenditore/ModificaProfilo
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ModificaProfilo([Bind("Id,RagioneSociale,Nome,Cognome,PartitaIva,Indirizzo,Citta,CAP,Provincia,Telefono,Pec,CodiceDestinatario")] Rivenditore rivenditore)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rivenditore);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RivenditoreExists(rivenditore.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Profilo));
        }
        return View(rivenditore);
    }

    private bool RivenditoreExists(int id)
    {
        return _context.Rivenditori.Any(e => e.Id == id);
    }
}
