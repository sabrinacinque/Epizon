using Microsoft.AspNetCore.Mvc;

namespace Epizon.Controllers
{
    public class SupportController : Controller
    {
        [HttpGet]
        public IActionResult ServizioClienti()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InviaRichiesta(string TipoProblema, string NumeroTelefono, string TestoRichiesta)
        {
            // Logica per inviare la richiesta o salvare nel database.
            // Puoi usare un servizio email o un sistema di gestione delle richieste.

            TempData["SuccessMessage"] = "La tua richiesta è stata inviata con successo. Verrai ricontattato al più presto.";
            return RedirectToAction("ServizioClienti");
        }
    }
}
