using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class Group
    {
        [Key] [InverseProperty("Recipient")]public int ID_Group { get; set; }
        [StringLength(70)] [Required] public string Name { get; set; }
        [ForeignKey("Game")] public int? IdGame { get; set; }
        [Required] public int Capacity { get; set; }
        [Required] [ForeignKey("Player")] public int IdAdmin { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Game Game { get; set; }
        [JsonIgnore] public virtual Player Player { get; set; }



    }
}