using Epizon.Data;
using Epizon.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Epizon.Controllers
{
    public class AccountController : Controller
    {
        private readonly EpizonContext _context;

        public AccountController(EpizonContext context)
        {
            _context = context;
        }

        // GET: /Account/RegisterCompratore
        [HttpGet]
        public IActionResult RegisterCompratore()
        {
            return View();
        }

        // POST: /Account/RegisterCompratore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCompratore(RegisterCompratoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Compratore
                {
                    Email = model.Email,
                    Nome = model.Nome,
                    Cognome = model.Cognome,
                    Indirizzo = model.Indirizzo,
                    Citta = model.Citta,
                    CAP = model.CAP,
                    Provincia = model.Provincia,
                    Telefono = model.Telefono,
                    Ruolo = "Compratore"
                };

                // Hash the password before saving it
                user.Password = HashPassword(model.Password);

                _context.Compratori.Add(user);
                await _context.SaveChangesAsync();

                // Sign in the user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Ruolo)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: /Account/RegisterRivenditore
        [HttpGet]
        public IActionResult RegisterRivenditore()
        {
            return View();
        }

        // POST: /Account/RegisterRivenditore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterRivenditore(RegisterRivenditoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Aggiungi un breakpoint qui o usa il logging per verificare i dati
                System.Diagnostics.Debug.WriteLine($"Email: {model.Email}");

                var user = new Rivenditore
                {
                    Email = model.Email,
                    Nome = model.Nome,
                    Cognome = model.Cognome,
                    RagioneSociale = model.RagioneSociale,
                    PartitaIva = model.PartitaIva,
                    Indirizzo = model.Indirizzo,
                    Citta = model.Citta,
                    CAP = model.CAP,
                    Provincia = model.Provincia,
                    Telefono = model.Telefono,
                    Pec = model.Pec,
                    CodiceDestinatario = model.CodiceDestinatario,
                    Ruolo = "Rivenditore"
                };

                // Hash the password before saving it
                user.Password = HashPassword(model.Password);
                
                

                _context.Rivenditori.Add(user);
                await _context.SaveChangesAsync();

                // Sign in the user
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Ruolo)
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: /Account/LoginCompratore
        [HttpGet]
        public IActionResult LoginCompratore()
        {
            return View();
        }

        // POST: /Account/LoginCompratore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginCompratore(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Compratori
                    .SingleOrDefaultAsync(u => u.Email == model.Email);

                if (user != null && VerifyPassword(model.Password, user.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, user.Ruolo)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        // GET: /Account/LoginRivenditore
        [HttpGet]
        public IActionResult LoginRivenditore()
        {
            return View();
        }

        // POST: /Account/LoginRivenditore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginRivenditore(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Rivenditori
                    .SingleOrDefaultAsync(u => u.Email == model.Email);

                if (user != null && VerifyPassword(model.Password, user.Password))
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Ruolo),
                new Claim("RivenditoreId", user.Id.ToString())  // Aggiungi l'ID del Rivenditore come claim
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Articoli");
                }

                ModelState.AddModelError(string.Empty, "Tentativo di login non valido.");
            }

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            // Implement a secure password hashing mechanism (e.g., using PBKDF2, bcrypt, or Argon2)
            // Here is a simple placeholder
            return password; // Replace with actual hashing logic
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Verify the entered password against the stored hashed password
            // Here is a simple placeholder
            return enteredPassword == storedPassword; // Replace with actual verification logic
        }
    }
}
