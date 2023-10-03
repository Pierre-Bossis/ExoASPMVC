using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExoASP.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        /*Pourquoi deux "password" -> En combinant le hash avec un Salt unique pour chaque utilisateur,
        même si deux utilisateurs ont le même mot de passe, leurs hash seront complètement différents en raison des salt différents.
        Cela rend beaucoup plus difficile pour un attaquant de précalculer les hachés pour une liste de mots courants.
        -> Le salt est d'abord combiné avec le mot de passe donné et ensuite celui ci est hashé.*/
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
