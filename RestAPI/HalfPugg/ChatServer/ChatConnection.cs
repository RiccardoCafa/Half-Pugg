using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Hubs
{
    public class ChatConnection
    {
       [Key] public string ConnectionID { get; set; }
        public bool Connected { get; set; }
    }
}