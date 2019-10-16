using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class Filter
    {
        [Key]
        public int ID_Filter { get; set; }
        [Required]
        [StringLength(100)]
        public string NameFilter { get; set; }
        public IList<Game> Games { get; set; }
        public IList<Hall> HallFilter { get; set; }
        public IList<Group> GroupFilter { get; set; }
    }



}