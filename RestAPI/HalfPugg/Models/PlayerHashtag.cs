using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class PlayerHashtag
    {
        [Key] public int ID { get; set; }
        [Required] public int IdHash { get; set; }
        [Required] public int IdPlayer { get; set; }
        [Required] public float Weight { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [ForeignKey("IdHash")] public virtual HashTag Hash { get; set; }
        [ForeignKey("IdPlayer")] public virtual Player Player { get; set; }

    }
}