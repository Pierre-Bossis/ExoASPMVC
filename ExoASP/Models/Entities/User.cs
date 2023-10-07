using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExoASP.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [MinLength(1)]
        public string Nom { get; set; }
        [MinLength(1)]
        public string Prenom { get; set; }
        [EmailAddress]
        [MinLength(8)]
        public string Email { get; set; }
        /* En combinant le hash avec un Salt unique pour chaque utilisateur,
        même si deux utilisateurs ont le même mot de passe, leurs hash seront complètement différents en raison des salt différents.
        Cela rend beaucoup plus difficile pour une attaque de précalculer les hash pour une liste de mots courants.
        -> Le salt est d'abord combiné avec le mot de passe donné et ensuite celui ci est hashé.*/
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public IEnumerable<Game> AddedGames { get; set; }

        public IEnumerable<UserGame>? JoinGames { get; set; }
        //public IEnumerable<Game> Games { get; set; }
    }
}
