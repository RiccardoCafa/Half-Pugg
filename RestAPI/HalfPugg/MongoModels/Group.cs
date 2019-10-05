using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Group
    {
        public int ID_Group { get; set; }
        public String Name { get; set; }
        public String ID_Game { get; set; }
        public int Capacity { get; set; }
        public List<> Components { get; set; }
        public int ID_Filter { get; set; }
    }
}