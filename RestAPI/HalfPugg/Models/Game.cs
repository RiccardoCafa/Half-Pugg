using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Game
    {
        [Key]
        [Required]
        public int ID_Game { get; set; }
        [Required]
        [StringLength(70)]
        public string Name { get; set; }
        [Required]
        [StringLength(70)]
        public string Description { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string EndPoint { get; set; }
        public IList<Filter> Filter { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }



    }
}