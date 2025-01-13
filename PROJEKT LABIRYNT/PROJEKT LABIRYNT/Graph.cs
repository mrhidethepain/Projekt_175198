using PROJEKT_LABIRYNT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace PROJEKT_LABIRYNT
{

    class Graph
    {
        private Dictionary<(int, int), NodeG> nodes = new Dictionary<(int, int), NodeG>();
        private List<Edge> edges = new List<Edge>();

        public void AddNode(NodeG node)
        {
            nodes[(node.Row, node.Col)] = node;
        }

        public void AddEdge(int row1, int col1, int row2, int col2)
        {
            var node1 = GetNode(row1, col1);
            var node2 = GetNode(row2, col2);

            if (node1 != null && node2 != null)
            {
                edges.Add(new Edge(node1, node2, 1));
                edges.Add(new Edge(node2, node1, 1)); // Graf nieskierowany
            }
        }

        public NodeG GetNode(int row, int col)
        {
            return nodes.ContainsKey((row, col)) ? nodes[(row, col)] : null;
        }

        public NodeG GetNeighbor(NodeG node, int dRow, int dCol)
        {
            var neighbor = GetNode(node.Row + dRow, node.Col + dCol);
            if (neighbor != null && edges.Any(e => e.Start == node && e.End == neighbor))
            {
                return neighbor;
            }
            return null; // Brak sąsiada lub jest ściana
        }

        public List<NodeG> Dijkstra(NodeG start, NodeG goal)
        {
            var distances = new Dictionary<NodeG, int>();
            var previousNodes = new Dictionary<NodeG, NodeG>();
            var nodesToVisit = new List<NodeG>();

            foreach (var node in nodes.Values)
            {
                distances[node] = int.MaxValue; // Ustawiamy początkowo dla każdego węzła nieskończoność
                previousNodes[node] = null;
                nodesToVisit.Add(node);
            }
            distances[start] = 0;

            while (nodesToVisit.Count > 0)
            {
                // Wybierz węzeł z najmniejszą odległością
                var currentNode = nodesToVisit.OrderBy(n => distances[n]).First();
                nodesToVisit.Remove(currentNode);

                // Sprawdzanie sąsiadów
                foreach (var edge in edges.Where(e => e.Start == currentNode))
                {
                    var neighbor = edge.End;
                    var newDist = distances[currentNode] + edge.Weight;
                    if (newDist < distances[neighbor])
                    {
                        distances[neighbor] = newDist;
                        previousNodes[neighbor] = currentNode;
                    }
                }
            }

            // Rekonstrukcja najkrótszej drogi
            var path = new List<NodeG>();
            var current = goal;
            while (current != null)
            {
                path.Insert(0, current);
                current = previousNodes[current];
            }

            return path;
        }
    }

    //class Graph
    //{
    //    public List<NodeG> nodes;
    //    public List<Edge> edges;

    //    public Graph()
    //    {
    //        this.nodes = new List<NodeG>();
    //        this.edges = new List<Edge>();
    //    }

    //    public Graph(Edge edge)
    //    {
    //        this.nodes = new List<NodeG>();
    //        AddEdge(edge);
    //    }

    //    public void AddEdge(Edge edge)
    //    {
    //        if (!this.edges.Contains(edge))
    //        {
    //            this.edges.Add(edge);
    //        }
    //        if (!this.nodes.Contains(edge.start))
    //        {
    //            this.nodes.Add(edge.start);
    //        }
    //        if (!this.nodes.Contains(edge.end))
    //        {
    //            this.nodes.Add(edge.end);
    //        }
    //    }

    //    public int NewNodesCount(Edge edge)
    //    {
    //        int count = 0;
    //        if (!this.nodes.Contains(edge.start))
    //        {
    //            count++;
    //        }
    //        if (!this.nodes.Contains(edge.end))
    //        {
    //            count++;
    //        }

    //        return count;
    //    }

    //    public void Join(Graph other)
    //    {
    //        foreach (Edge edge in other.edges)
    //        {
    //            this.AddEdge(edge);
    //        }
    //    }

    //    public List<Element> Dijkstra(NodeG start)
    //    {
    //        var table = new List<Element>();
    //        foreach (NodeG node in this.nodes)
    //        {
    //            if (node == start)
    //            {
    //                table.Add(new Element(start, 0, null));
    //            }
    //            else
    //            {
    //                table.Add(new Element(node, int.MaxValue, null));
    //            }
    //        }
    //        var S = new List<NodeG>();
    //        S.Add(start);

    //        while (S.Count < this.nodes.Count)
    //        {
    //            var candidate = table.Where(e => !S.Contains(e.node))
    //                                 .OrderBy(e => e.weight)
    //                                 .First();

    //            S.Add(candidate.node);

    //            var neighbours = this.edges.Where(e => e.start == candidate.node && !S.Contains(e.end)).ToList();

    //            foreach (var neighbour in neighbours)
    //            {
    //                var nextElement = table.Find(e => e.node == neighbour.end);

    //                int newWeight = candidate.weight + neighbour.weight;

    //                if (newWeight < nextElement.weight)
    //                {
    //                    nextElement.weight = newWeight;
    //                    nextElement.before = candidate.node;
    //                }
    //            }


    //        }
    //        return table;
    //    }

    //}
}
