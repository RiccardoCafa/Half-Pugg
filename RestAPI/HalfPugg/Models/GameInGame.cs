using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class GameInGame
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [StringLength(300)]
        [Required]
        public Classification_Gamer Classification { get; set; }
        [Required]
        public PlayerGame IdGame { get; set; }
        [Required]        
        public float Points { get; set; }
        
        public DateTime CreateAt { get; set; }
        
        public DateTime AlteredAt { get; set; }
    }
}