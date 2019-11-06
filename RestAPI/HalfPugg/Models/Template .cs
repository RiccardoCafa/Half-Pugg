using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class Template
    {
        [Key] public int ID_Template { get; set; }
        [Required] public int IdGame { get; set; }
        [Required] [StringLength(100)] public string Path { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Game Game { get; set; }
    }
}