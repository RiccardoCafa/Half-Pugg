using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class Group
    {
        [Key] public int ID_Group { get; set; }
        [StringLength(70)] [Required] public string Name { get; set; }
        [Required] public int IdGame { get; set; }
        [Required] public int Capacity { get; set; }
        [Required] public int IdAdmin { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [ForeignKey("IdGame")] public virtual Game Game { get; set; }
        [ForeignKey("IdAdmin")] public virtual Player Admin { get; set; }



    }
}