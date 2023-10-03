using System.ComponentModel.DataAnnotations;

namespace ExoASP.Models.Forms
{
    public class RegisterForm
    {
        [Required(ErrorMessage = $"Champ {nameof(Nom)} requis.")]
        [MinLength(1)]
        public string Nom { get; set; }
        [Required(ErrorMessage = $"Champ {nameof(Prenom)} requis.")]
        [MinLength(1)]
        public string Prenom { get; set; }
        [Required(ErrorMessage = $"Champ {nameof(Email)} requis.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = $"Champ {nameof(Password)} requis.")]
        [MinLength(8)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string Confirmation { get; set; }
    }
}
