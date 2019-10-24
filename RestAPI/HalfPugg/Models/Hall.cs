﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Hall
    {
        [Key] public int ID_Hall { get; set; }
        [StringLength(70)] public string Name { get; set; }
        [Required] public int IdGame { get; set; }
        [Required] public int Capacity { get; set; }

        public IList<Filter> Filters { get; set; }
        [Required] public Player Admin { get; set; }
        public IList<Player> Components { get; set; }
        public IList<MessageHall> Chat { get; set; }
        
        public DateTime CreateAt { get; set; }
        
        public DateTime AlteredAt { get; set; }
        public virtual Game game { get; set; }
    }
}