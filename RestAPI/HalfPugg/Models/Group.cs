using System;
using System.Collections.Generic;


namespace HalfPugg.Models
{
    public class Group
    {
        public int ID_Group { get; set; }
        public string Name { get; set; }
        public Game Game { get; set; }
        public int Capacity { get; set; }

        public int ID_Filter { get; set; }
        public Gamer Admin { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<Gamer> Components { get; set; }
        public IList<MessageGroup> Chat { get; set; }

            

    }
}