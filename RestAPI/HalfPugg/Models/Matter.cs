using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Matter
    {
        [Key]
        public int ID_Matter { get; set; }
        [Required]
        public string HashTag { get; set; }
    }
}