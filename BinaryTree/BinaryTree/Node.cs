using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;

namespace BinaryTree
{
    internal class Node
    {
        public List<Node> Child { get; } = new List<Node>();

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
        public event PropertyChangedEventHandler PropertyChanged;

        private void onChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Node(int uniqNumber, Node parent)
        {
            UniqNumber = uniqNumber;
            Parent = parent;
        }

        public int UniqNumber { set; get; }

        public int Depth => GetDepth();

        public Node Parent { get; }

        public Node LeftChild
        {
            set
            {
                onChange("Child"); 
                Child.Insert(0, value);
            }
            get => Child.ElementAtOrDefault(0);
        }

        public Node RightChild
        {
            set
            {
                onChange("Child"); 
                Child.Insert(1, value);
            }
            get => Child.ElementAtOrDefault(1);
        }

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
    }
}