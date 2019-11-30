using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Text;
using System;
public enum Check
{
    white, gray, black
}


public class Graph<VERTICE, EDGE_DATA, VERT_ID> where VERT_ID : IComparable
{
    #region structure

    public delegate double calcWeight(EDGE_DATA data);
    public delegate bool edgeFilter(Edge data);
    public delegate bool conectCondition(Vertice a, Vertice b);
    public delegate bool verticeFilter(Vertice data);
    public delegate T verticeIdentifier<T>(Vertice data);


    public class Edge
    {
        public Vertice connectedTo;
        public double weight;
        public EDGE_DATA data;
    }
    public class Vertice
    {
        public VERTICE info;
        public VERT_ID id;
        public List<Edge> connections = new List<Edge>();

    }
    #endregion


    private bool digraph = false;
    private calcWeight calcWeightFnc;

    public conectCondition ConectConditionDefault = null;

    public ConcurrentDictionary<VERT_ID, Vertice> vertices { get; private set; }

    public int vertCount { get { return vertices.Count; } }

    public Graph(calcWeight weightFnc = null, bool digraph = false, int capacity = 0)
    {
        this.digraph = digraph;
        calcWeightFnc = weightFnc;
        vertices = new ConcurrentDictionary<VERT_ID, Vertice>(Environment.ProcessorCount * 2, capacity);
    }

    public bool AddVertice(VERTICE info, VERT_ID Id)
    {
        if (vertices.ContainsKey(Id)) return false;
        Vertice v = new Vertice() { id = Id, info = info };
        vertices[Id] = v;
        return true;
    }

    public IEnumerable<Vertice> GetVertices(verticeFilter filter = null)
    {
        if (filter == null) return vertices.Values;
        return vertices.Values.Where(x => filter(x));
    }

    public void AddAresta(VERT_ID from, VERT_ID to, EDGE_DATA data, conectCondition condition = null)
    {
        if (condition == null) condition = ConectConditionDefault;
        if (condition?.Invoke(vertices[from], vertices[to]) == false) return;

        double w = (calcWeightFnc !=null) ? calcWeightFnc(data):0;

        Edge e = new Edge
        {
            connectedTo = vertices[to],
            data = data,
            weight = w
        };

        vertices[from].connections.Add(e);

        if (!digraph)
        {

            Edge e2 = new Edge
            {
                connectedTo = vertices[from],
                data = data,
                weight = w
            };
            vertices[to].connections.Add(e2);
        }

    }

    public void AddAresta(VERT_ID from, VERT_ID to,double Weight = 1,  conectCondition condition = null)
    {
        if (condition == null) condition = ConectConditionDefault;
        if (condition?.Invoke(vertices[from], vertices[to]) == false) return;

        double w = Weight;

        Edge e = new Edge
        {
            connectedTo = vertices[to],
            data = default,
            weight = w
        };

        vertices[from].connections.Add(e);

        if (!digraph)
        {

            Edge e2 = new Edge
            {
                connectedTo = vertices[from],
                data = default,
                weight = w
            };
            vertices[to].connections.Add(e2);
        }

    }

    public IEnumerable<Vertice> Bfs(VERT_ID from, edgeFilter filter = null)
    {
        if (!vertices.ContainsKey(from)) yield return null;

        Dictionary<VERT_ID, Check> visited = new Dictionary<VERT_ID, Check>(vertCount);

        foreach (var v in vertices.Values)
        {
            visited.Add(v.id, Check.white);
        }

        Queue<Vertice> queue = new Queue<Vertice>();

        queue.Enqueue(vertices[from]);

        while (queue.Count > 0)
        {
            Vertice atu = queue.Dequeue();

            visited[atu.id] = Check.black;

            foreach (var v in atu.connections)
            {
                if (visited[v.connectedTo.id] == Check.white)
                {
                    if (filter?.Invoke(v) != false)
                    {
                        queue.Enqueue(v.connectedTo);
                        visited[v.connectedTo.id] = Check.gray;
                    }

                }

            }

            yield return atu;
        }


    }

    public Dictionary<VERT_ID, double> ShortPath(VERT_ID from)
    {
        int vtCount = vertCount;
        Dictionary<VERT_ID, bool> visited = new Dictionary<VERT_ID, bool>(vtCount);
        Dictionary<VERT_ID, double> distances = new Dictionary<VERT_ID, double>(vtCount);
        int finded = 1;
        
        foreach (var item in vertices.Keys)
        {
            visited.Add(item, false);
            distances.Add(item, double.MaxValue);
        }
        distances[from] = 0;

        VERT_ID getShort()
        {
            double min = double.MaxValue;
            VERT_ID ret = from;
            foreach (VERT_ID id in vertices.Keys)
            {
                if (!visited[id])
                {
                    if (distances[id] == 0) return id;
                    if (distances[id] <= min)
                    {
                        ret = id;
                        min = distances[id];
                    }
                }

            }
            finded++;
            return ret;
        }

        while (finded != vtCount)
        {
            VERT_ID id = getShort();
            foreach (var e in vertices[id].connections)
            {
                if (distances[id] + e.weight < distances[e.connectedTo.id]) distances[e.connectedTo.id] = distances[id] + e.weight;
            }
            visited[id] = true;
        }

        return distances;
    }
    public string ToNet<T>(verticeIdentifier<T> identifier)
    {
        StringBuilder file = new StringBuilder();
        Dictionary<VERT_ID, uint> vert_index = new Dictionary<VERT_ID, uint>(vertCount);
        HashSet<long> edgeIds = new HashSet<long>(vertCount);

        long getEdgeID(uint a,uint b)
        {
            if (b < a)
            {
                uint aux = a;
                a = b;
                b = aux;    
            }
            return long.Parse($"{a}{b}");
        }

        file.AppendLine($"*vertices {vertCount}");
        uint i = 1;
        foreach (var v in vertices)
        {
            file.AppendLine($"{i} \"{identifier(v.Value)}\"");
            vert_index.Add(v.Key, i);
            i++;
        }
        if (digraph) file.AppendLine("*arcs");
        else file.AppendLine("*edges");

        if (!digraph)
        {
            if (calcWeightFnc != null)
            {
                foreach (var v in vertices)
                {
                    foreach (var arc in v.Value.connections)
                    {
                        long n = getEdgeID(vert_index[v.Key], vert_index[arc.connectedTo.id]);
                        if (edgeIds.Contains(n)) continue;
                        file.AppendLine($"{vert_index[v.Key]} {vert_index[arc.connectedTo.id]} {arc.weight.ToString().Replace(',', '.')}");
                        edgeIds.Add(n);
                    }

                }
            }
            else
            {
                foreach (var v in vertices)
                {
                    foreach (var arc in v.Value.connections)
                    {
                        long n = getEdgeID(vert_index[v.Key], vert_index[arc.connectedTo.id]);
                        if (edgeIds.Contains(n)) continue;
                        file.AppendLine($"{vert_index[v.Key]} {vert_index[arc.connectedTo.id]}");
                        edgeIds.Add(n);
                    }
                }
            }
        }
        else
        {
            if (calcWeightFnc != null)
            {
                foreach (var v in vertices)
                {
                    foreach (var arc in v.Value.connections)
                    {

                        file.AppendLine($"{vert_index[v.Key]} {vert_index[arc.connectedTo.id]} {arc.weight.ToString().Replace(',', '.')}");
                    }

                }
            }
            else
            {
                foreach (var v in vertices)
                {
                    foreach (var arc in v.Value.connections)
                    {
                        file.AppendLine($"{vert_index[v.Key]} {vert_index[arc.connectedTo.id]}");
                    }
                }
            }
        }

        return file.ToString();
    }
    public string ToNet()
    {
        StringBuilder file = new StringBuilder();
        Dictionary<VERT_ID, uint> vert_index = new Dictionary<VERT_ID, uint>(vertCount);
        HashSet<long> edgeIds = new HashSet<long>(vertCount);

        long getEdgeID(uint a, uint b)
        {
            if (b < a)
            {
                uint aux = a;
                a = b;
                b = aux;
            }
            return long.Parse($"{a}{b}");
        }

        file.AppendLine($"*vertices {vertCount}");
      
        uint i = 1;
        foreach (var v in vertices)
        {
            file.AppendLine($"{i} \"{v.Key}\"");
            vert_index.Add(v.Key, i);
            i++;
        }
        
        if (digraph) file.AppendLine("*arcs");
        else file.AppendLine("*edges");

        if (!digraph)
        {
            if (calcWeightFnc != null)
            {
                foreach (var v in vertices)
                {
                    foreach (var arc in v.Value.connections)
                    {
                        long n = getEdgeID(vert_index[v.Key], vert_index[arc.connectedTo.id]);
                        if (edgeIds.Contains(n)) continue;
                        file.AppendLine($"{vert_index[v.Key]} {vert_index[arc.connectedTo.id]} {arc.weight.ToString().Replace(',', '.')}");
                        edgeIds.Add(n);
                    }

                }
            }
            else
            {
                foreach (var v in vertices)
                {
                    foreach (var arc in v.Value.connections)
                    {
                        long n = getEdgeID(vert_index[v.Key], vert_index[arc.connectedTo.id]);
                        if (edgeIds.Contains(n)) continue;
                        file.AppendLine($"{vert_index[v.Key]} {vert_index[arc.connectedTo.id]}");
                        edgeIds.Add(n);
                    }
                }
            }
        }
        else
        {
            if (calcWeightFnc != null)
            {
                foreach (var v in vertices)
                {
                    foreach (var arc in v.Value.connections)
                    {

                        file.AppendLine($"{vert_index[v.Key]} {vert_index[arc.connectedTo.id]} {arc.weight.ToString().Replace(',', '.')}");
                    }

                }
            }
            else
            {
                foreach (var v in vertices)
                {
                    foreach (var arc in v.Value.connections)
                    {
                        file.AppendLine($"{vert_index[v.Key]} {vert_index[arc.connectedTo.id]}");
                    }
                }
            }
        }


        return file.ToString();
    }
    public override string ToString()
    {
        return ToNet();
    }
}

public class Graph<VERTICE, EDGE_DATA> : Graph<VERTICE, EDGE_DATA, uint>
{
    public Graph(calcWeight weightFnc, bool digraph = false) : base(weightFnc, digraph)
    {

    }
}

public class Graph<VERTICE> : Graph<VERTICE, object, uint>
{
    public Graph( bool digraph = false) : base(null, digraph)
    {

    }
}

public class Graph : Graph<string, object, uint>
{
    public Graph(bool digraph = false) : base(null, digraph)
    {

    }
}


