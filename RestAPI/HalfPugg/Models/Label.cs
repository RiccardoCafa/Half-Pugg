using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Label
    {
        public int ID { get; set; }
        public Filter ID_Filter { get; set; }
        [Required]
        [StringLength(50)]
        public string NameLabel { get; set; }
    }
}