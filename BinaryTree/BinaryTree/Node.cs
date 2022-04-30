using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Node
    {
        public int UniqNumber { set; get; }

        public int Depth 
        {   
            get
            {
                return getDepth();
            }
        }

        public Node Parent { private set; get; }
        public Node LeftChild { set; get; }
        public Node RightChild { set; get; }
        public string data { set; get; }

        private int getDepth()
        {
            int depth = 0;
            Node node = this;
            while (node.Parent != null)
            {
                node = node.Parent;
                depth++;
            }

            return depth;
        }


        public Node(int uniqNumber, Node parent, string data)
        {
            if (data.ToString().Contains("*") ^ data.ToString().Contains(";"))
                throw new Exception("Data cannot contain '*' or ';' character!");

            this.UniqNumber = uniqNumber;
            this.data = data;
            this.LeftChild = null;
            this.RightChild = null;
            this.Parent = parent;
        }

        public Node(int uniqNumber, Node parent)
        {
            this.UniqNumber = uniqNumber;
            this.Parent = parent;
        }
    }
}
