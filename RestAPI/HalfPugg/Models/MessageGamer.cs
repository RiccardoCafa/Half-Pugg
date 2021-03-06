﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class MessageGamer
    {
        [Key]
        public int ID_Message { get; set; }
        [StringLength(400)]
        [Required]
        public string Content { get; set; }
        public DateTime Send_Time{ get; set; }
        [Required]
        public Gamer ID_User { get; set; }
        [Required]
        public Gamer ID_Recipient { get; set; }
        [Required]
        public char Status { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public DateTime AlteredAt { get; set; }
    }
}