using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Message
    {
        public int ID_Message { get; set; }
        public string Content { get; set; }
        public DateTime Send_Time{ get; set; }
        public int ID_User { get; set; }
        public int ID_Recipient { get; set; }
        public char Status { get; set; }
    }
}