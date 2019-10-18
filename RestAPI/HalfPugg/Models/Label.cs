﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Label
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public Filter ID_Filter { get; set; }
        [Required]
        [StringLength(50)]
        public string NameLabel { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public DateTime AlteredAt { get; set; }
    }
}