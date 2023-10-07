using ExoASP.Interfaces;
using ExoASP.Models.Entities;
using Microsoft.EntityFrameworkCore;

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


        public async Task AddGameToUser(int gameId, User user)
        {
            var game = await _context.games.FirstOrDefaultAsync(x => x.Id == gameId);
            if (game is not null)
            {
                UserGame usergame = new UserGame();
                usergame.User = user;
                usergame.UserId = user.Id;
                usergame.GameId = gameId;
                usergame.Game = game;
                usergame.DateAchat = DateTime.Now;
                usergame.EstOccasion = false;

                await _context.UserGame.AddAsync(usergame);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            //problème ici, ca ramène le joinGame uniquement des créateurs de jeux.
            //var games = await _context.games.Include(g => g.Creator).ThenInclude(u => u.JoinGames).ToListAsync();
            var games = await _context.games
                    .Include(g => g.Creator) // Inclure les créateurs de jeux
                    .ThenInclude(u => u.JoinGames) // Inclure les JoinGames des créateurs
                    .Include(g => g.JoinUsers) // Inclure les joinUser des jeux eux-mêmes
                    .ToListAsync();

            if (games is not null)
            {
                return games;
            }
            return null;
        }
        public async Task<User> MyGames(User user)
        {
            var user2 = await _context.Users.Include(x => x.JoinGames).FirstOrDefaultAsync(x => x.Id == user.Id);
            if (user2 is not null)
            {
                return user2;
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
