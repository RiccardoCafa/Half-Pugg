using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Gamer
    {
        public String Name { get; set; }
        public String LastName { get; set; }
        public int ID { get; set; }
        public string Nickname { get; set; }
        public string Bio { get; set; }
        public char Type { get; set; }
        public int ID_Branch { get; set; }
        public string HashPassword { get; set; }
        public char Sex { get; set; }
        public string Genre { get; set; }
    }
}