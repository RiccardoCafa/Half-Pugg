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
        [StringLength(50)]
        public string Nickname { get; set; }
        [Required]
        [StringLength(100)]
        public string HashPassword { get; set; }
        [StringLength(300)]
        public string Bio { get; set; }
        public string  Email { get; set; }
        public DateTime Birthday { get; set; }
        [StringLength(100)]
        public string ImagePath { get; set; }
        [Required]
        public char Type { get; set; }
        public int ID_Branch { get; set; }        
        public char Sex { get; set; }
        [StringLength(100)]
        public string Genre { get; set; }
        public IList<Gamer> Matches { get; set; }        
        public IList<HashTag> Hashtags { get; set; }        
        public IList<Classification_Gamer> Classification { get; set; }
        public IList<Group> Groups { get; set; }
        public IList<MessageHall> Halls { get; set; } 
    }
}