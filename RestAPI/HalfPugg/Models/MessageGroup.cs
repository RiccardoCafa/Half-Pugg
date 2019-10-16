﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class MessageGroup
    {
        public int ID { get; set; }
        [StringLength(500)]
        public string Content { get; set; }
        public DateTime Send_Time { get; set; }
        public DateTime View_Time { get; set; }
        public Gamer ID_User { get; set; }
        public Group ID_Recipient { get; set; }
        public char Status { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
    }
}