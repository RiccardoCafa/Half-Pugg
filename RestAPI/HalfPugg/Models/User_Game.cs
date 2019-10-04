using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class User_Game
    {
        [Key]
        public int ID_Game_Gamer { get; set; }
        [Required]
        public int ID_Game { get; set; }
        [Required]
        public int ID_Gamer { get; set; }
        [Required]
        public string ID_API { get; set; }

    }
}