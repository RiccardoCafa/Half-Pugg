using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Match
    {
        [Key] public int ID { get; set; }
        [Required] public int IdPlayer1 { get; set; }
        [Required] public int IdPlayer2 { get; set; }
        [Required] public bool Status { get; set; }
        [Required] public float Weight { get; set; }
        
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }

        public virtual Player Player1 { get; set; }
        public virtual Player Player2 { get; set; }
    }
}