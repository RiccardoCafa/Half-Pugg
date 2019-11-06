using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class Numbered
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Filter")] public int ID_Filter { get; set; }
        [Required] public float Number { get; set; }        
        public DateTime CreateAt { get; set; }        
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Filter Filter { get; set; }
    }
}