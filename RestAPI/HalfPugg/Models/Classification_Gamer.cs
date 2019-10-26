using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class Classification_Gamer
    {
        [Key] public int ID { get; set; }
        [Required] [StringLength(50)] public string Description { get; set; }
        
        public DateTime CreateAt { get; set; }
        
        public DateTime AlteredAt { get; set; }

    }
}