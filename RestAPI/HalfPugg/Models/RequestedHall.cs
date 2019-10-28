using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class RequestedHall
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        [Required] [ForeignKey("Sala")] public int IdSala { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime ComfirmedTime { get; set; }
        [Required] public char Status { get; set; }
        [ForeignKey("Filters")] public int IdFilters { get; set; }
        public virtual Player Player { get; set; }
        public virtual Group Sala { get; set; }
        public virtual Filter Filters { get; set; }
    }

}