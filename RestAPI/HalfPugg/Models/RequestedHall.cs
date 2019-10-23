using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class RequestedHall
    {
        [Key] public int ID { get; set; }
        [Required] public int IdPlayer { get; set; }
        [Required] public int IdSala { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime ComfirmedTime { get; set; }
        [Required] public char Status { get; set; }
        public Filter Filters { get; set; }        
        public virtual Gamer Player { get; set; }
        public virtual Hall Sala { get; set; }
    }

}