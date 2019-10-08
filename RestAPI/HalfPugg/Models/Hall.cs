﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.MongoModels
{
    public class Hall
    {
        public int ID_Hall { get; set; }
        public string Name { get; set; }
        public string ID_Game { get; set; }
        public int Capacity { get; set; }

        public IList<Filter> Filter { get; set; }
        public Gamer Admin { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<Gamer> Components { get; set; }
        public IList<MessageHall> Chat { get; set; }
    }
}