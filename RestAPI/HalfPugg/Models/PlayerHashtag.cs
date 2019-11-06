using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class PlayerHashtag
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Hash")] public int IdHash { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        [Required] public float Weight { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual HashTag Hash { get; set; }
        [JsonIgnore] public virtual Player Player { get; set; }

    }
}