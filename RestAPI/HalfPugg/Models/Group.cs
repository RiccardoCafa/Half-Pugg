using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HalfPugg.Models
{
    public class Group
    {
        [Key]
        public int ID_Group { get; set; }
        [StringLength(70)]
        public string Name { get; set; }
        public Game Game { get; set; }
        public int Capacity { get; set; }        
        public Gamer Admin { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<Gamer> Components { get; set; }
        public IList<MessageGroup> Chat { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }



    }
}