using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class PlayerHashtag
    {
        [Key] public int ID { get; set; }
        [Required] public int IdHash { get; set; }
        [Required] public int IdPlayer { get; set; }
        [Required] public float Weight { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }

        public virtual HashTag Hash { get; set; }
        public virtual Gamer Player { get; set; }

    }
}