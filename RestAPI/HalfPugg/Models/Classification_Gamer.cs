using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Classification_Gamer
    {
        [Key]
        public int ID_Classification { get; set; }
        [Required]
        public int ID_Gamer { get; set; }
        [Required]
        public float Points { get; set; }
    }
}