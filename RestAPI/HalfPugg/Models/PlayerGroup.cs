using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class PlayerGroup
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        [Required] [ForeignKey("Group")] public int IdGroup { get; set; }
        public virtual Player Player { get; set; }
        public virtual Group Group { get; set; }
    }
}