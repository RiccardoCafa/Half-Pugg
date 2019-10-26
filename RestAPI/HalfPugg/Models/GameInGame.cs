using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class GameInGame
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Classification")] public int IdClassification { get; set; }
        [Required] [ForeignKey("PlayerGame")] public int IdPlayerGame { get; set; }
        [Required] public float Points { get; set; }        
        public DateTime CreateAt { get; set; }        
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Classification_Gamer Classification { get; set; }
        [JsonIgnore] public virtual PlayerGame PlayerGame { get; set; }
    }
}