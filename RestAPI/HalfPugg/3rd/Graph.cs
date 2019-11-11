using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg._3rd
{
    public class Graph<VERTICE,WEIGHT,EDGE_DATA>
    {
        public delegate WEIGHT calcWeight(EDGE_DATA data);


        calcWeight calcWeightFnc;

        public class Edge 
        {
            public Vertice connectedTo;
            public WEIGHT weight;
            public EDGE_DATA data;
        }
        public class Vertice
        {
            public VERTICE info;
            public int id;
            public List<Edge> connections;
            
        }

        public Dictionary<int, Vertice> vertices;

        public Graph(calcWeight weightFnc)
        {
            calcWeightFnc = weightFnc;
            vertices = new Dictionary<int, Vertice>();
        }

        public void AddVertice(VERTICE info)
        {
            int id = vertices.Count;
            Vertice v = new Vertice() { id = id, info = info, connections = new List<Edge>() };
            vertices.Add(id, v);
        }
    }
}