using Epizon.Data;
using Epizon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


public class MetodoPagamentoController : Controller
{
    private readonly EpizonContext _context;

    public MetodoPagamentoController(EpizonContext context)
    {
        _context = context;
    }
    // GET: MetodoPagamento
    public async Task<IActionResult> Index()
    {
        var metodiPagamento = await _context.MetodoPagamento.Include(mp => mp.Utente).ToListAsync();
        return View("~/Views/Payment/PaymentMethods.cshtml", metodiPagamento);
    }

    // GET: MetodoPagamento/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var metodoPagamento = await _context.MetodoPagamento
            .Include(mp => mp.Utente)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (metodoPagamento == null)
        {
            return NotFound();
        }

        return View(metodoPagamento);
    }

    // GET: MetodoPagamento/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MetodoPagamento/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Tipo,NumeroCarta,Intestatario,DataScadenza,CodiceSicurezza,IBAN,NomeBanca")] MetodoPagamento metodoPagamento)
    {
        if (ModelState.IsValid)
        {
            // Assumendo che tu stia usando i claims per l'autenticazione
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Ottieni l'ID dell'utente loggato
            metodoPagamento.UtenteId = int.Parse(userId); // Imposta l'ID dell'utente

            _context.Add(metodoPagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View("~/Views/Payment/Create.cshtml");
    }

    // GET: MetodoPagamento/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var metodoPagamento = await _context.MetodoPagamento.FindAsync(id);
        if (metodoPagamento == null)
        {
            return NotFound();
        }
        return View(metodoPagamento);
    }

    // POST: MetodoPagamento/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,UtenteId,Tipo,NumeroCarta,Intestatario,DataScadenza,CodiceSicurezza,IBAN,NomeBanca")] MetodoPagamento metodoPagamento)
    {
        if (id != metodoPagamento.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(metodoPagamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodoPagamentoExists(metodoPagamento.Id))
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
        return View(metodoPagamento);
    }

    // GET: MetodoPagamento/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var metodoPagamento = await _context.MetodoPagamento
            .Include(mp => mp.Utente)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (metodoPagamento == null)
        {
            return NotFound();
        }

        return View(metodoPagamento);
    }

    // POST: MetodoPagamento/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var metodoPagamento = await _context.MetodoPagamento.FindAsync(id);
        _context.MetodoPagamento.Remove(metodoPagamento);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MetodoPagamentoExists(int id)
    {
        return _context.MetodoPagamento.Any(e => e.Id == id);
    }
}
