using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace HalfPugg.Models
{
    public class ClassificationInMatch
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Match")] public int IdMatch { get; set; }
        [Required] [ForeignKey("Classification")] public int IdClassification { get; set; }
        [Required] public float Points { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Match Match { get; set; }
        [JsonIgnore] public virtual Classification_Match Classification { get; set; }
    }
}