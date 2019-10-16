using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Match
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public Gamer Player1 { get; set; }
        [Required]
        public Gamer Player2 { get; set; }
        [Required]
        public char Status { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public DateTime AlteredAt { get; set; }
    }
}