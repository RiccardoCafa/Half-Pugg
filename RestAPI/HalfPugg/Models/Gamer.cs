using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace HalfPugg.Models
{
    public class Gamer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(70)]
        public string LastName { get; set; }
        [Required]
        [StringLenght(50)]
        public string Nickname { get; set; }
        [Required]
        [StringLenght(100)]
        public MD5 HashPassword { get; set; }
        [StringLenght(300)]
        public string Bio { get; set; }

        public DateTime Birthday { get; set; }
        [StringLenght(100)]
        public string ImagePath { get; set; }

        [Required]
        public char Type { get; set; }

        public int ID_Branch { get; set; }
        
        public char Sex { get; set; }
        [StringLenght(100)]
        public string Genre { get; set; }

        public IList<Gamer> Matches { get; set; }
        
        public IList<Matter> Hashtags { get; set; }
        
        public IList<Game> Games { get; set; }

        public IList<Classification_Gamer> Classification { get; set; }

        public IList<Groups> Groups { get; set; }

        public IList<Halls> Halls { get; set; }

        
        
    }
}