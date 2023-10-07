using ExoASP.Models.Entities;

namespace ExoASP.Interfaces
{
    public interface IGameRepository
    {
        Task Create(Game game);
        Task<IEnumerable<Game>> GetAll();
        Task AddGameToUser(int id,User user);
        Task<User> GetUserById(string id);

        Task<User> MyGames(User user);
    }
}
