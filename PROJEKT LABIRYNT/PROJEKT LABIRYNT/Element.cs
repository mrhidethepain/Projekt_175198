using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKT_LABIRYNT
{
    class Element
    {
        public NodeG node;
        public int weight;
        public NodeG before;

        public Element(NodeG node)
        {
            this.node = node;
            this.weight = int.MaxValue;
            this.before = null;
        }

        public Element(NodeG node, int weight, NodeG before)
        {
            this.node = node;
            this.weight = weight;
            this.before = before;
        }
    }
}
