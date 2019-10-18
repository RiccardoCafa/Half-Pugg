using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class RequestedGroup
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public Gamer Player { get; set; }
        [Required]
        public Group Sala { get; set; }
        [Required]
        public DateTime RequestedTime { get; set; }
        [Required]
        public DateTime ComfirmedTime { get; set; }
        [Required]
        public char Status { get; set; }
        public Filter Filters { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public DateTime AlteredAt { get; set; }
    }
}