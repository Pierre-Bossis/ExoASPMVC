using ExoASP.Interfaces;
using ExoASP.Models.Entities;
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
            var games = await _repo.GetAll();
            return View(games);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            //repo
            var user = await _repo.GetUserById(HttpContext.Session.GetString("UserId"));
            game.User = user;

            await _repo.Create(game);

            return RedirectToAction("Index");
        }
    }
}
