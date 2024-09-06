﻿using Epizon.Data;
using Epizon.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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
                // Cerca l'utente nel database
                var user = await _context.Compratori
                    .SingleOrDefaultAsync(u => u.Email == model.Email);

                // Se l'utente non esiste o la password è errata
                if (user == null || !VerifyPassword(model.Password, user.Password))
                {
                    // Aggiungi un messaggio di errore al ModelState
                    ModelState.AddModelError(string.Empty, "Email o password errati.");
                }
                else
                {
                    // Crea i claims
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Ruolo),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
            }

            // Ritorna alla vista del login con il ModelState che include il messaggio di errore
            return View(model);
        }


        // GET: /Account/LoginRivenditore
        [HttpGet]
        public IActionResult LoginRivenditore()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginRivenditore(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Cerca l'utente nel database
                var user = await _context.Rivenditori
                    .SingleOrDefaultAsync(u => u.Email == model.Email);

                // Se l'utente non esiste o la password è errata
                if (user == null || !VerifyPassword(model.Password, user.Password))
                {
                    // Aggiungi un messaggio di errore al ModelState
                    ModelState.AddModelError(string.Empty, "Email o password errati.");
                }
                else
                {
                    // Crea i claims
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Ruolo),
                new Claim("RivenditoreId", user.Id.ToString())  // Aggiungi l'ID del Rivenditore come claim
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    // Redirect to the HomeRivenditore view
                    return RedirectToAction("HomeRivenditore", "Home");
                }
            }

            // Ritorna alla vista del login con il ModelState che include il messaggio di errore
            return View(model);
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // O qualsiasi altra pagina di redirezione
        }
        // Azione per visualizzare il profilo dell'utente
        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var email = User.Identity.Name;
            var compratore = await _context.Compratori
                                           .FirstOrDefaultAsync(c => c.Email == email);

            if (compratore == null)
            {
                return NotFound();
            }

            return View(compratore);
        }


        // GET: /Account/EditProfile
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var email = User.Identity.Name;
            if (email == null)
            {
                return RedirectToAction("LoginCompratore", "Account");
            }

            var compratore = await _context.Compratori.FirstOrDefaultAsync(c => c.Email == email);

            if (compratore == null)
            {
                return NotFound();
            }

            return View(compratore);
        }


        // POST: /Account/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([Bind("Id,Nome,Cognome,Indirizzo,Citta,CAP,Provincia,Telefono,Email")] Compratore model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var email = User.Identity.Name;
            var user = await _context.Compratori.FirstOrDefaultAsync(c => c.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            // Update only the editable fields
            user.Nome = model.Nome;
            user.Cognome = model.Cognome;
            user.Indirizzo = model.Indirizzo;
            user.Citta = model.Citta;
            user.CAP = model.CAP;
            user.Provincia = model.Provincia;
            user.Telefono = model.Telefono;
            user.Email = model.Email;

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle errors, e.g., duplicate email
                ModelState.AddModelError("", "Impossibile salvare le modifiche. Riprova.");
                return View(model);
            }

            return RedirectToAction("Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteProfile()
        {
            // Ottieni l'ID dell'utente dal claim

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                var claims = User.Claims.Select(c => new { c.Type, c.Value });
                // Log i claims per verificare cosa è disponibile
                return RedirectToAction("Index", "Home");
            }

            // Recupera l'utente dal database
            var user = await _context.Compratori.FindAsync(int.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }

            // Elimina l'utente dal database
            _context.Compratori.Remove(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Gestione eccezioni, ad esempio, log o messaggio di errore
                return StatusCode(500, "Errore durante l'eliminazione dell'account.");
            }

            // Logout l'utente e redirigi alla home page o login
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LoginOrRegister()
        {
            // Logica per la visualizzazione della pagina di login o registrazione
            return View();
        }


    }


}
