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
        [Required] public int IdSala { get; set; }
        [Required] public DateTime RequestedTime { get; set; }
        [Required] public DateTime ComfirmedTime { get; set; }
        [Required] public char Status { get; set; }
        public Filter Filters { get; set; }        
        public DateTime CreateAt { get; set; }       
        public DateTime AlteredAt { get; set; }
        public virtual Gamer Player { get; set; }
        public virtual Gamer Sala { get; set; }
    }
}