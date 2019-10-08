using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Game
    {
        [Key]
        [Required]
        public int ID_Game { get; set; }
        [Required]
        [StringLength(70)]
        public string Name { get; set; }
        [Required]
        [StringLength(70)]
        public string Description { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string EndPoint { get; set; }

        public IList<Gamer> Gamers { get; set; }

        public IList<Classification_Game> Classifications { get; set; }

        public IList<Filter> Filter { get; set; }

        public IList<HashTag> Hashtags { get; set; }

        public IList<Filter> Filters { get; set; }

    }
}