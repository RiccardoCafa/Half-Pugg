using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Classification_User
    {
        [Key]
        public int ID_Classification { get; set; }
        [Required]
        public string Description { get; set; }

    }
}