using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class PlayerGame
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [StringLength(300)]
        [Required]
        public string Description { get; set; }
        [Required]
        public Game IdGame { get; set; }
        [Required]
        public Gamer IdGamer { get; set; }
        [Required]
        public string IdAPI { get; set; }
        [Required]
        public float Weight { get; set; }
    }
}