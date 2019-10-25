using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class PlayerHall
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        [Required] [ForeignKey("Hall")] public int IdHall { get; set; }
        public virtual Player Player { get; set; }
        public virtual Hall Hall { get; set; }
    }
}