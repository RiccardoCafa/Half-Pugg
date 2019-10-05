using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.MongoModels
{
    public class Template
    {
        public int ID_Template { get; set; }
        public int ID_Game { get; set; }
        public string Path { get; set; }
    }
}