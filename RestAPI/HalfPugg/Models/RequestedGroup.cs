using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    //model para um admin de grupo solicitar ao um player
    public class RequestedGroup
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        [Required] [ForeignKey("Group")] public int IdGroup { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime ComfirmedTime { get; set; }
        [Required] [StringLength(1)] public string Status { get; set; }
        [JsonIgnore] public virtual Player Player { get; set; }
        [JsonIgnore] public virtual Group Group { get; set; }
        
    }
}