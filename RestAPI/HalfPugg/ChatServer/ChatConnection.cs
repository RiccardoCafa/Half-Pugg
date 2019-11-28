using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HalfPugg.Models;

namespace HalfPugg.Hubs
{
    public class ChatConnection
    {
       [Key] public string ConnectionID { get; set; }
        public bool Connected { get; set; }

        [ForeignKey("Player")] public int Player_ID { get; set; }
        public virtual Player Player { get; set; }
    }
}