using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Hall
    {
        [Key] public int ID_Hall { get; set; }
        [StringLength(70)] public string Name { get; set; }
        [Required] public Game game { get; set; }
        [Required] public int Capacity { get; set; }

        public IList<Filter> Filters { get; set; }
        [Required] public Gamer Admin { get; set; }
        public IList<Gamer> Components { get; set; }
        public IList<MessageHall> Chat { get; set; }
        
        public DateTime CreateAt { get; set; }
        
        public DateTime AlteredAt { get; set; }
    }
}