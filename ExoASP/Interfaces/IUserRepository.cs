using ExoASP.Models.Entities;
using ExoASP.Models.Forms;

namespace ExoASP.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(RegisterForm registerForm);

        Task<User> Login(LoginForm loginForm);
    }
}
