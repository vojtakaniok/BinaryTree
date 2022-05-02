using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;

namespace BinaryTree
{
    internal class Node:INotifyPropertyChanged
    {
        public Dictionary<int, Node> ChildKeyValuePair { get; } = new Dictionary<int, Node>();
        public List<Node> Child => new List<Node>(ChildKeyValuePair.Values);

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

        private void OnChange(string propertyName)
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

        public Node Parent { get; set; }

        public Node LeftChild
        {
            set
            {
                ChildKeyValuePair[0]= value;
                OnChange("Child");
            }
            get
            {
                Node value = null;
                ChildKeyValuePair.TryGetValue(0, out value);
                return value;
            }
        }

        public Node RightChild
        {
            set
            {
                ChildKeyValuePair[1] = value;
                OnChange("Child");
            }
            get {
            Node value = null;
            ChildKeyValuePair.TryGetValue(1, out value);
            return value;
            }
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