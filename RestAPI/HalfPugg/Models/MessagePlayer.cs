﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class MessagePlayer
    {
        [Key]
        public int ID { get; set; }
        [StringLength(500)] public string Content { get; set; }

        [Required] public DateTime Send_Time { get; set; }
        
        [Required] [ForeignKey("Sender")] public int ID_Sender { get; set; }
        [Required] [ForeignKey("Destination")] public int ID_Destination { get; set; }
        public MessageStatus Status { get; set; }
        [JsonIgnore] public virtual Player Sender { get; set; }
        [JsonIgnore] public virtual Player Destination { get; set; }


    }
}