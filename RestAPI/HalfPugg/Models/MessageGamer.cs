﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class MessageGamer
    {
        [Key] public int ID_Message { get; set; }

        [StringLength(400)] [Required]
        public string Content { get; set; }
        [Required] public Gamer ID_User { get; set; }
        [Required] public Gamer ID_Recipient { get; set; }
        [Required] public char Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
    }
}