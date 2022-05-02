﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BinaryTree
{
    internal class Node : INotifyPropertyChanged
    {
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

        private readonly Dictionary<int, Node> _childKeyValuePair = new Dictionary<int, Node>();
        public List<Node> Child => new List<Node>(_childKeyValuePair.Values);

        public int UniqNumber { set; get; }

        public int Depth => GetDepth();

        public Node Parent { get; set; }

        public Node LeftChild
        {
            set
            {
                _childKeyValuePair[0] = value;
                OnChange("Child");
            }
            get
            {
                Node value = null;
                _childKeyValuePair.TryGetValue(0, out value);
                return value;
            }
        }

        public Node RightChild
        {
            set
            {
                _childKeyValuePair[1] = value;
                OnChange("Child");
            }
            get
            {
                Node value;
                _childKeyValuePair.TryGetValue(1, out value);
                return value;
            }
        }

        public string Data { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChange(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

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