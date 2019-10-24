using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Filter Filters { get; set; }        
        public virtual Player Player { get; set; }
        public virtual Player Player2 { get; set; }
    }
}