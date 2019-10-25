using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class PlayerHall
    {
        [Key] public int ID { get; set; }
        [Required] public int IdPlayer { get; set; }
        [Required] public int IdHall { get; set; }
        [ForeignKey("IdPlayer")] public virtual Player Player { get; set; }
        [ForeignKey("IdHall")] public virtual Hall Hall { get; set; }
    }
}