using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Topic
    {
        [Key]
        public int ID_Topic { get; set; }
        [Required]
        public int ID_Game { get; set; }
        [Required]
        public int ID_Matter { get; set; }
    }
}