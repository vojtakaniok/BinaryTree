using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Node<T>
    {
        public int UniqNumber { set; get; }

        public Node<T> Parent { set; get; }
        public Node<T> LeftChild { set; get; }
        public Node<T> RightChild { set; get; }
        public T data { set; get; }

        public Node(int uniqNumber, Node<T> parent, T data)
        {
            this.UniqNumber = uniqNumber;
            this.data = data;
            this.LeftChild = null;
            this.RightChild = null;
            this.Parent = parent;
        }

        public Node(int uniqNumber, Node<T> parent)
        {
            this.UniqNumber = uniqNumber;
            this.Parent = parent;
        }
    }
}
