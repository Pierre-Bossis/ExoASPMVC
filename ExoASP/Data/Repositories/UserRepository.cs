using ExoASP.Interfaces;
using ExoASP.Models.Entities;
using ExoASP.Models.Forms;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ExoASP.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(LoginForm loginForm)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == loginForm.Email);

            if (user is null)
            {
                return null;
            }

            //décrypte le hashage
            using var hmac = new HMACSHA512(user.PasswordSalt);

            //Hash du password reçu pour vérifier dans une boucle que tous les octets sont identiques, Si une différence est détectée c'est que c'est pas bon
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginForm.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return null;
                }

            }

            return user;
        }

        public async Task<User> Register(RegisterForm registerForm)
        {
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Nom = registerForm.Nom,
                Prenom = registerForm.Prenom,
                Email = registerForm.Email,
                //PasswordHash est initialisé avec le résultat du hachage du mot de passe
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerForm.Password)),
                //PasswordSalt est initialisé avec la clé utilisée pour le hachage du mot de passe
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
