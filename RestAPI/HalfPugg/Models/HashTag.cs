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
        [StringLength(70)]
        public string Hashtag { get; set; }
      
        public IList<Gamer> Gamers { get; set; }

        public IList<Game> Games { get; set; }
    }
}