using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Template
    {
        [Key]
        public int ID_Template { get; set; }
        [Required]
        public Game game { get; set; }
        [Required]
        [StringLength(100)]
        public string Path { get; set; }
    }
}