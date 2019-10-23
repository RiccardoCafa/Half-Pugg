using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Template
    {
        [Key] public int ID_Template { get; set; }
        [Required] public int IdGame { get; set; }
        [Required] [StringLength(100)] public string Path { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        public virtual Game Game { get; set; }
    }
}