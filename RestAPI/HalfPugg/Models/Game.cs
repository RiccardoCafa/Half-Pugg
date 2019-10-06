using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Game
    {
        [Key]
        [Required]
        public int ID_Game { get; set; }
        [Required]
        [StringLenght(70)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }



    }
}