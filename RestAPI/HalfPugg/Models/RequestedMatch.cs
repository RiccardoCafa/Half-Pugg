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
        [Required] public int IdPlayer { get; set; }
        [Required] public int IdPlayer2 { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime ComfirmedTime { get; set; }
        [Required] public char Status { get; set; }
        public int IdFilters { get; set; }
        [ForeignKey("IdPlayer")] public virtual Player Player1 { get; set; }
        [ForeignKey("IdPlayer2")] public virtual Group Player2 { get; set; }
        [ForeignKey("IdFilters")] public virtual Filter Filters { get; set; }
    }
}