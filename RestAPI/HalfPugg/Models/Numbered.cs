using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class Numbered
    {
        [Key] public int ID { get; set; }
        [Required] public Filter ID_Filter { get; set; }
        [Required] public float Number { get; set; }        
        public DateTime CreateAt { get; set; }        
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Filter IDFilter { get; set; }
    }
}