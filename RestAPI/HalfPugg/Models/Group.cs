using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class Group
    {
        [Key] public int ID { get; set; }
        [Required] [StringLength(70)] public string Name { get; set; }
        [Required] public int Capacity { get; set; }

        public DateTime CreateAt { get; set; }

        [ForeignKey("Game")] public int IdGame { get; set; }
        [Required] [ForeignKey("Admin")] public int IdAdmin { get; set; }

        [JsonIgnore] public virtual Game Game { get; set; }
        [JsonIgnore] public virtual Player Admin { get; set; }

        [JsonIgnore] public ICollection<PlayerGroup> Integrants { get; set; }
        [JsonIgnore] public ICollection<MessageGroup> Messages { get; set; }

    }
}