using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class RequestedMatch
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player1")] public int IdPlayer { get; set; }
        [Required] [ForeignKey("Player2")] public int IdPlayer2 { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime ComfirmedTime { get; set; }
        [Required] public char Status { get; set; }
        [ForeignKey("Filters")] public int IdFilters { get; set; }
        [JsonIgnore] public virtual Player Player1 { get; set; }
        [JsonIgnore] public virtual Group Player2 { get; set; }
        [JsonIgnore] public virtual Filter Filters { get; set; }
    }
}