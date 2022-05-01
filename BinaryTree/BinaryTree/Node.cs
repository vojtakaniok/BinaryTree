using System;

namespace BinaryTree
{
    internal class Node
    {
        public int UniqNumber { set; get; }

        public int Depth => GetDepth();

        public Node Parent { private set; get; }
        public Node LeftChild { set; get; }
        public Node RightChild { set; get; }
        public string Data { set; get; }

        private int GetDepth()
        {
            var depth = 0;
            var node = this;
            while (node.Parent != null)
            {
                node = node.Parent;
                depth++;
            }

            return depth;
        }


        public Node(int uniqNumber, Node parent, string data)
        {
            if (data.Contains("*") ^ data.Contains(";"))
                throw new Exception("Data cannot contain '*' or ';' character!");

            UniqNumber = uniqNumber;
            Data = data;
            LeftChild = null;
            RightChild = null;
            Parent = parent;
        }

        public Node(int uniqNumber, Node parent)
        {
            UniqNumber = uniqNumber;
            Parent = parent;
        }
    }
}
