using Epizon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Epizon.Data;

namespace Epizon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EpizonContext _context;

        public HomeController(ILogger<HomeController> logger, EpizonContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Elenco delle categorie
            var categorie = new List<CategoriaViewModel>
            {
                new CategoriaViewModel { Nome = "Alimentari e bevande", ImmagineUrl = "alimentari.jpg" },
                new CategoriaViewModel { Nome = "Farmacia e cura della persona", ImmagineUrl = "farmacia.jpg" },
                new CategoriaViewModel { Nome = "Animali domestici", ImmagineUrl = "animali.jpg" },
                new CategoriaViewModel { Nome = "Moda e bellezza", ImmagineUrl = "moda.jpg" },
                new CategoriaViewModel { Nome = "Casa", ImmagineUrl = "casa.jpg" },
                new CategoriaViewModel { Nome = "Dispositivi ed elettronica", ImmagineUrl = "elettronica.jpg" },
                new CategoriaViewModel { Nome = "Musica, video e giochi", ImmagineUrl = "musica.jpg" },
                new CategoriaViewModel { Nome = "Libri e lettura", ImmagineUrl = "libri.jpg" },
                new CategoriaViewModel { Nome = "Giochi, bambini e prima infanzia", ImmagineUrl = "giochi.jpg" },
                new CategoriaViewModel { Nome = "Auto e moto", ImmagineUrl = "auto.jpg" },
                new CategoriaViewModel { Nome = "Ufficio e professionisti", ImmagineUrl = "ufficio.jpeg" },
                new CategoriaViewModel { Nome = "Sport", ImmagineUrl = "sport.jpeg" }
            };

            return View(categorie);
        }

        public async Task<IActionResult> Categoria(string categoria)
        {
            if (string.IsNullOrEmpty(categoria))
            {
                return NotFound();
            }

            var articoli = await _context.Articoli
                .Where(a => a.Categoria == categoria)
                .ToListAsync();

            if (articoli == null || articoli.Count == 0)
            {
                return NotFound();
            }

            ViewData["Categoria"] = categoria;
            return View(articoli);
        }


        public IActionResult HomeRivenditore()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    // ViewModel per rappresentare le categorie
    public class CategoriaViewModel
    {
        public string Nome { get; set; }
        public string ImmagineUrl { get; set; }
    }

 
}
