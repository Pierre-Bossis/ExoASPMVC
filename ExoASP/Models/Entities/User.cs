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
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
