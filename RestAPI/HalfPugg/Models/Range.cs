using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Range
    {
        [Key] public int ID { get; set; }
        [Required] public Filter ID_Filter { get; set; }
        public float Max { get; set; }
        public float Min { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
    }
}