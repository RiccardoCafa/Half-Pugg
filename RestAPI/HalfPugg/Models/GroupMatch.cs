using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HalfPugg.Models
{
    public class GroupMatch
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Group")] public int IdGroup { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        [Required] public bool Status { get; set; }
        [Required] public float Weight { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Group Group { get; set; }
        [JsonIgnore] public virtual Player Player { get; set; }
    }
}