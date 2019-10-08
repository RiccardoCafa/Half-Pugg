using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Label
    {
        public int ID { get; set; }
        public Filter ID_Filter { get; set; }
        [Required]
        [StringLenght(50)]
        public string Label { get; set; }
    }
}