using ExoASP.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExoASP.Models.ViewModels
{
    public class GameView
    {

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Editeur { get; set; }
        public DateTime AnneeDeSortie { get; set; }
        public DateTime DateAjout { get; set; }
        public User Creator { get; set; }
        public Guid CreatorId { get; set; }

        public IEnumerable<UserGame>? JoinUsers { get; set; }
    }
}
