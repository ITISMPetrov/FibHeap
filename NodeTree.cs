using System;
using System.Collections.Generic;
using System.Text;

namespace FibonacciHeap
{
    class NodeTree
    {
        public int NodeData { get; set; }
        public NodeTree Parent { get; set; }
        public LinkedList<NodeTree> Childrens { get; set; }
        public int Degree { get; set; }
        public bool Mark { get; set; }

        public NodeTree(int data)
        {
            NodeData = data;
            Childrens = new LinkedList<NodeTree>();
        }
    }
}
