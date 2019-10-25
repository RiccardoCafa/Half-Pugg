using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class RequestedMatch
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player1")] public int IdPlayer { get; set; }
        [Required] [ForeignKey("Player2")] public int IdPlayer2 { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime ComfirmedTime { get; set; }
        [Required] public char Status { get; set; }
        [ForeignKey("Filters")] public int IdFilters { get; set; }
        public virtual Player Player1 { get; set; }
        public virtual Group Player2 { get; set; }
        public virtual Filter Filters { get; set; }
    }
}