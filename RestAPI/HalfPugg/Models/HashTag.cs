using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class HashTag
    {
        [Key] public int ID_Matter { get; set; }
        [Required] [StringLength(70)] public string Hashtag { get; set; }
        [JsonIgnore] public virtual ICollection<Game> Games { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
    }
}