using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Numbered
    {
        public int ID { get; set; }
        public Filter ID_Filter { get; set; }
        public float Number { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
    }
}