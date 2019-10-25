using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class PlayerGroup
    {
        [Key] public int ID { get; set; }
        [Required] public int IdPlayer { get; set; }
        [Required] public int IdGroup { get; set; }
        [ForeignKey("IdPlayer")] public virtual Player Player { get; set; }
        [ForeignKey("IdGroup")] public virtual Group Group { get; set; }
    }
}