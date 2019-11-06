using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class PlayerGroup
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        [Required] [ForeignKey("Group")] public int IdGroup { get; set; }
        [JsonIgnore] public virtual Player Player { get; set; }
        [JsonIgnore] public virtual Group Group { get; set; }
    }
}