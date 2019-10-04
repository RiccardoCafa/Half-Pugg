using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Game_Topic
    {
        [Key]
        public int ID_Gamer_Topic { get; set; }
        [Required]
        public int ID_Gamer { get; set; }
        [Required]
        public int ID_Topic { get; set; }
    }
}