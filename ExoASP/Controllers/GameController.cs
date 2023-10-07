using ExoASP.Interfaces;
using ExoASP.Models.Entities;
using ExoASP.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExoASP.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepository _repo;

        public GameController(IGameRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _repo.GetUserById(HttpContext.Session.GetString("UserId"));
            var games = await _repo.GetAll();
            return View(Tuple.Create(games,user));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize, HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            //repo
            var user = await _repo.GetUserById(HttpContext.Session.GetString("UserId"));
            game.Creator = user;

            await _repo.Create(game);

            return RedirectToAction("Index");
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> AddGameToList(int gameId)
        {
            //traitement pour ajouter
            var user = await _repo.GetUserById(HttpContext.Session.GetString("UserId"));
            await _repo.AddGameToUser(gameId, user);

            return RedirectToAction("Index"); // rediriger vers la liste des jeux
        }

        [Authorize]
        public async Task<IActionResult> ShowGameList()
        {
            //var user = await _repo.GetUserById(HttpContext.Session.GetString("UserId"));
            //var games = await _repo.MyGames(user);
            return View();
        }
    }
}
