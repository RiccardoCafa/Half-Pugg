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
        public Gamer Player1 { get; set; }
        public Gamer Player2 { get; set; }
        public char Status { get; set; }
        public float Weight { get; set; }
    }
}