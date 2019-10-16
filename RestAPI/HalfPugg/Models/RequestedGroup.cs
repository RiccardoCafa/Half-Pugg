using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class RequestedGroup
    {
        public int ID { get; set; }
        public Gamer Player { get; set; }
        public Group Sala { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime ComfirmedTime { get; set; }
        public char Status { get; set; }
        public Filter Filters { get; set; }
    }
}