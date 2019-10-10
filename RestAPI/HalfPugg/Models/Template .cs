using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Template
    {
        public int ID_Template { get; set; }
        [Required]
        public Game game { get; set; }
        [Required]
        [StringLenght(100)]
        public string Path { get; set; }
    }
}