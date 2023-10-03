using ExoASP.Data;
using ExoASP.Models;
using ExoASP.Models.Entities;
using ExoASP.Models.Forms;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace ExoASP.Controllers
{
    public class AuthController : Controller
    {
        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContext;

        public AuthController(DataContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

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
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerForm.Password)),
                    PasswordSalt = hmac.Key
                };
                _context.users.Add(user);
                await _context.SaveChangesAsync();

                //Méthode utilisée pour se connecter après inscription
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

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginForm.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    ViewBag.ErrorMessage += "Mauvais Email ou Password";
                    return View();
                }

            }

            //Méthode utilisée pour se connecter après vérification
            SignIn(user);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            HttpContext?.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        private void SignIn(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.Prenom} {user.Nom}"),
                // Ajoutez d'autres revendications au besoin
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
