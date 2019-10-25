using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class GameInGame
    {
        [Key] public int ID { get; set; }
        [Required] public int IdClassification { get; set; }
        [Required] public int IdPlayerGame { get; set; }
        [Required] public float Points { get; set; }        
        public DateTime CreateAt { get; set; }        
        public DateTime AlteredAt { get; set; }
        [ForeignKey("IdClassification")] public virtual Classification_Gamer Classification { get; set; }
        [ForeignKey("IdPlayerGame")] public virtual PlayerGame PlayerGame { get; set; }
    }
}