﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HalfPugg.Models
{
    public class PlayerGame
    {
        [Key]
        [Required] public int ID { get; set; }
        [StringLength(300)] [Required] public string Description { get; set; }                
        [Required] [ForeignKey("Game")] public int IDGame { get; set; }
        [Required] [ForeignKey("Gamer")] public int IDGamer { get; set; }
        [Required] [Index(IsUnique = true)] public string IdAPI { get; set; }
        [Required] public float Weight { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [ForeignKey("IDGamer")] public virtual Player Gamer { get; set; }
        [ForeignKey("IDGame")] public virtual Game Game { get; set; }
    }
}