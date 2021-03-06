﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace HalfPugg.Models
{
    //model para os player solicitarem a entrada a um grupo
    public class PlayerRequestedGroup
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayerRequest { get; set; }
        [Required] [ForeignKey("Group")] public int IdGroup { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime ComfirmedTime { get; set; }
        [Required] [StringLength(1)] public string Status { get; set; }
        [JsonIgnore] public virtual Player Player { get; set; }
        [JsonIgnore] public virtual Group Group { get; set; }
        
    }
}