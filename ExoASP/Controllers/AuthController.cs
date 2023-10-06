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
using ExoASP.Interfaces;

namespace ExoASP.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _repo;

        public AuthController(IUserRepository repo)
        {
            _repo = repo;

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
                User user = await _repo.Register(registerForm);
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
            User user = await _repo.Login(loginForm);
            if(user is null)
            {
                ViewBag.ErrorMessage = "Mauvais Login ou Password.";
                return View(loginForm);
            }
            SignIn(user);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            HttpContext?.SignOutAsync();
            HttpContext?.Session.Clear();
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
                //new Claim(ClaimTypes.NameIdentifier, $"{user.Id}")
                // Ajoutez d'autres revendications au besoin
            };
            //Une identité est créée en utilisant la liste de revendications (claims) et le schéma d'authentification(voir program.cs)
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //Représentation de l'Identité de l'Utilisateur
            var principal = new ClaimsPrincipal(identity);

            //log l'User dans l'application
            HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            HttpContext?.Session.SetString("UserId",$"{user.Id}");
        }
    }
}
