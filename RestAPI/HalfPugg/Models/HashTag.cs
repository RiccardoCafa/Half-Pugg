using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class HashTag
    {
        [Key]
        public int ID_Matter { get; set; }
        [Required]
      
        public IList<Gamer> Gamers { get; set; }

        public IList<Game> Games { get; set; }
    }
}