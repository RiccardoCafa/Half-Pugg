using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class MenssageHall
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime Send_Time { get; set; }
        public DateTime View_Time { get; set; }
        public Gamer ID_User { get; set; }
        public Hall ID_Recipient { get; set; }
        public char Status { get; set; }
    }
}