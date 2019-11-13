using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class ClassificationPlayer
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        [Required] [ForeignKey("JudgePlayer")] public int IdJudgePlayer { get; set; }
        [Required] [ForeignKey("Classification")] public int IdClassification { get; set; }
        [Required] public float Points { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Player Player { get; set; }
        [JsonIgnore] public virtual Player JudgePlayer { get; set; }
        [JsonIgnore] public virtual Classification_Gamer Classification { get; set; }
    }
}