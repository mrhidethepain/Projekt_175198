﻿using PROJEKT_LABIRYNT;
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
                edges.Add(new Edge(node2, node1, 1));
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
            return null;
        }

        public List<NodeG> Dijkstra(NodeG start, NodeG goal)
        {
            var distances = new Dictionary<NodeG, int>();
            var previousNodes = new Dictionary<NodeG, NodeG>();
            var nodesToVisit = new List<NodeG>();

            foreach (var node in nodes.Values)
            {
                distances[node] = int.MaxValue; 
                previousNodes[node] = null;
                nodesToVisit.Add(node);
            }
            distances[start] = 0;

            while (nodesToVisit.Count > 0)
            {

                var currentNode = nodesToVisit.OrderBy(n => distances[n]).First();
                nodesToVisit.Remove(currentNode);

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

}
