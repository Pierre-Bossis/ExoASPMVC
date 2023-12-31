﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExoASP.Models.Entities
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MinLength(1)]
        public string Nom { get; set; }
        [MinLength(1)]
        public string Editeur { get; set; }
        public DateTime AnneeDeSortie { get; set; }
        public DateTime DateAjout { get; set; }
        public User Creator { get; set; }
        public Guid CreatorId { get; set; }

        public IEnumerable<UserGame>? JoinUsers { get; set; }
        //public IEnumerable<User> Users { get; set; }
    }
}