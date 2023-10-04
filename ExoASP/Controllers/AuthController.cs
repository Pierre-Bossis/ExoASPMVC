using ExoASP.Data;
using ExoASP.Models.Entities;
using ExoASP.Models.Forms;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExoASP.Controllers
{
    public class AuthController : Controller
    {
        private readonly DataContext _context;



        public AuthController(DataContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return RedirectToAction("Register");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterForm registerForm)
        {
            if (ModelState.IsValid)
            {
                using var hmac = new HMACSHA512();

                var user = new User
                {
                    Nom = registerForm.Nom,
                    Prenom = registerForm.Prenom,
                    Email = registerForm.Email,
                    //PasswordHash est initialisé avec le résultat du hachage du mot de passe
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerForm.Password)),
                    //PasswordSalt est initialisé avec la clé utilisée pour le hachage du mot de passe
                    PasswordSalt = hmac.Key
                };
                _context.users.Add(user);
                await _context.SaveChangesAsync();

                SignIn(user);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginForm loginForm)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Email == loginForm.Email);

            if (user is null)
            {
                ViewBag.ErrorMessage += "Mauvais Email ou Password";
                return View();
            }

            //décrypte le hashage
            using var hmac = new HMACSHA512(user.PasswordSalt);

            //Hash du password reçu pour vérifier dans une boucle que tous les octets sont identiques, Si une différence est détectée c'est que c'est pas bon
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginForm.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    ViewBag.ErrorMessage += "Mauvais Email ou Password";
                    return View();
                }

            }
            SignIn(user);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            HttpContext?.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Créé les revendications et l'identité de l'utilisateur ET le connecte sur l'application.
        /// </summary>
        /// <param name="user"></param>
        private void SignIn(User user)
        {
            //créé la liste des revendications de l'user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.Prenom} {user.Nom}"),
                // Ajoutez d'autres revendications au besoin
            };
            //Une identité est créée en utilisant la liste de revendications (claims) et le schéma d'authentification(voir program.cs)
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //Représentation de l'Identité de l'Utilisateur
            var principal = new ClaimsPrincipal(identity);

            //log l'User dans l'application
            HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
