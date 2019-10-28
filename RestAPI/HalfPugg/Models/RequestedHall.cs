﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HalfPugg.Models
{
    public class RequestedHall
    {
        [Key] public int ID { get; set; }
        [Required] [ForeignKey("Player")] public int IdPlayer { get; set; }
        [Required] [ForeignKey("Sala")] public int IdSala { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime ComfirmedTime { get; set; }
        [Required] [StringLength(1)] public string Status { get; set; }
        [ForeignKey("Filters")] public int IdFilters { get; set; }
        public virtual Player Player { get; set; }
        [JsonIgnore] public virtual Group Sala { get; set; }
        [JsonIgnore] public virtual Filter Filters { get; set; }
    }

}