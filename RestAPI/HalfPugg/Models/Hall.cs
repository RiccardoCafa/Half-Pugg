using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class Hall
    {
        [Key] public int ID { get; set; }
        [Required] [StringLength(70)] public string Name { get; set; }
        [Required] public int Capacity { get; set; }

        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }

        [Required] [ForeignKey("Game")] public int IdGame { get; set; }
        [Required] [ForeignKey("Admin")] public int IdAdmin { get; set; }
        [JsonIgnore] public virtual Game Game { get; set; }
        [JsonIgnore] public virtual Player Admin { get; set; }


        [JsonIgnore] public IList<Filter> Filters { get; set; }
        [JsonIgnore] public ICollection<PlayerHall> Integrants { get; set; }
        [JsonIgnore] public ICollection<MessageHall> Messages { get; set; }
    }
}