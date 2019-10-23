using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class ClassificationPlayer
    {
        [Key] public int ID { get; set; }
        [Required] public Gamer Player { get; set; }
        [Required] public Classification_Gamer Classification { get; set; }
        [Required] public float Points { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }

    }
}