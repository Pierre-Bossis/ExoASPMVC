using ExoASP.Interfaces;
using ExoASP.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExoASP.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _context;

        public GameRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Create(Game game)
        {
            if (game is not null)
            {
                _context.games.AddAsync(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            var games = await _context.games.Include(g=>g.User).ToListAsync();

            if(games is not null)
            {
                return games;
            }
            return null;
        }









        public async Task<User> GetUserById(string id)
        {
            Guid IdGuid = Guid.Parse(id);
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == IdGuid);
            if (user is not null)
            {
                return user;
            }
            return null;
        }
    }
}
