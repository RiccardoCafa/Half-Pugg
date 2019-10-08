using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Gamer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public string HashPassword { get; set; }
        
        public string Bio { get; set; }
        [Required]
        public char Type { get; set; }

        public int ID_Branch { get; set; }
        
        public char Sex { get; set; }
        
        public string Genre { get; set; }

        public IList<Gamer> Matches { get; set; }
        
        public IList<Matter> Hashtags { get; set; }
        
        public IList<Game> Games { get; set; }

        public IList<Classification_User> Classification { get; set; }

        public IList<Groups> Groups { get; set; }

        public IList<Halls> Halls { get; set; }
    }
}