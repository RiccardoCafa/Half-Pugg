using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class Filter
    {
        [Key] public int ID_Filter { get; set; }
        [Required] [StringLength(100)] public string NameFilter { get; set; }
        [JsonIgnore] public virtual IList<Game> Games { get; set; }
        [JsonIgnore] public virtual IList<Hall> HallFilter { get; set; }
        [JsonIgnore] public virtual IList<Group> GroupFilter { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
    }



}