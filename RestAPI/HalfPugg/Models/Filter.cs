using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.MongoModels
{
    public class Filter
    {
        public int ID_Filter { get; set; }
        public string Filter { get; set; }
        public IList<Game> Games { get; set; }
    }

   

}