using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HalfPugg.Models
{
    public class PlayerGame
    {
        [Key]
        [Required] public int ID { get; set; }
        [StringLength(300)] [Required] 
        public string Description { get; set; }
        public Game Game { get; set; }       
        public Gamer Gamer { get; set; }
        [Required] public int IDGamer { get; set; }         
        [Required] public int IDGame { get; set; }
        [Required] [Index(IsUnique = true)] public string IdAPI { get; set; }
        [Required] public float Weight { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }

    }
}