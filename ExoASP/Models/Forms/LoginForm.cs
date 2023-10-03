using System.ComponentModel.DataAnnotations;

namespace ExoASP.Models.Forms
{
    public class LoginForm
    {
        [Required(ErrorMessage = $"Champ Email requis.")]
        public string Email { get; set; }

        [Required(ErrorMessage = $"Champ Password requis.")]
        public string Password { get; set; }
    }
}
