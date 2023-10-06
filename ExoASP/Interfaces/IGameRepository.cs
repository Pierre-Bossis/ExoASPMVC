using ExoASP.Models.Entities;

namespace ExoASP.Interfaces
{
    public interface IGameRepository
    {
        Task Create(Game game);
        Task<IEnumerable<Game>> GetAll();
        Task<User> GetUserById(string id);
    }
}
