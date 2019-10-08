using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Classification_Gamer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public IList<Gamer> Gamers { get; set; }

    }
}