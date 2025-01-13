using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKT_LABIRYNT
{
    class Edge
    {
        public NodeG Start;
        public NodeG End;
        public int Weight;

        public Edge(NodeG start, NodeG end, int weight)
        {
            this.Start = start;
            this.End = end;
            this.Weight = weight;
        }
    }
}
    
