using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.MongoModels
{
    public class Filter
    {
        public int ID_Filter { get; set; }
        public List<string> Filters { get; set; }
        public int ID_Game { get; set; }
    }
}