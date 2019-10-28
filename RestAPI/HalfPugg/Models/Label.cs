using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class Label
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Filter")] public int ID_Filter { get; set; }
        [Required] [StringLength(50)] public string NameLabel { get; set; }        
        public DateTime CreateAt { get; set; }        
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Filter Filter { get; set; }
    }
}