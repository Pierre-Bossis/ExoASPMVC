namespace ExoASP.Models.Entities
{
    public class UserGame
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        public DateTime DateAchat { get; set; }
        public bool EstOccasion { get; set; }
    }
}
