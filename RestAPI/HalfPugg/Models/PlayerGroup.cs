﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace HalfPugg.Models
{
    public class PlayerGroup
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Group")] public int IdGroup { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? AlteredAt { get; set; }
        [JsonIgnore] public virtual Group Group { get; set; }
        [JsonIgnore] public virtual Player Player { get; set; }
    }
}