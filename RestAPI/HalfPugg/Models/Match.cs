using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class Match
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player1")] public int IdPlayer1 { get; set; }
        [Required] [ForeignKey("Player2")] public int IdPlayer2 { get; set; }
        [Required] public bool Status { get; set; }
        [Required] public float Weight { get; set; }        
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Player Player1 { get; set; }
        [JsonIgnore] public virtual Player Player2 { get; set; }
    }
}