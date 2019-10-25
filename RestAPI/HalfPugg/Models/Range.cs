using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Range
    {
        [Key] public int ID { get; set; }
        [Required] public int ID_Filter { get; set; }
        [Required] public float Max { get; set; }
        [Required] public float Min { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [ForeignKey("ID_Filter")] public virtual Filter IDFilter { get; set; }
    }
}