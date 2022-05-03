using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryTree
{
    internal class Tree
    {
        private int _amountOfNodes;

        public Tree(string data)
        {
            _amountOfNodes = 0;
            Root.Insert(0, new Node(_amountOfNodes, null, data));
            _amountOfNodes++;
        }

        public Tree()
        {
            _amountOfNodes = 0;
            Root.Insert(0, new Node(_amountOfNodes, null));
            _amountOfNodes++;
        }

        public List<Node> Root { get; } = new List<Node>();
        public int MaxLength { get; private set; }

        public int AddNode(Node node, string data, bool startWithRight = false)
        {
            if (data.Contains("*") ^ data.Contains(";"))
                throw new Exception("Data cannot contain '*' or ';' character!");
            if (MaxLength < data.Length)
                MaxLength = data.Length;
            if (node == null)
                throw new Exception("Node not found");

            if (!startWithRight)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new Node(_amountOfNodes, node, data);
                    _amountOfNodes++;
                    return _amountOfNodes - 1;
                }

                throw new Exception("Left Child already present");
            }

            if (node.RightChild == null)
            {
                node.RightChild = new Node(_amountOfNodes, node, data);
                _amountOfNodes++;
                return _amountOfNodes - 1;
            }

            throw new Exception("Right Child Already present");
        }

        public Node FindNode(int uniqNumber)
        {
            var queue = new Queue<Node>();
            var visited = new HashSet<Node>();
            var root = Root.ElementAt(0);
            if (root.UniqNumber == uniqNumber)
                return root;
            queue.Enqueue(root);


            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (visited.Contains(node))
                    continue;
                if (node.UniqNumber == uniqNumber)
                    return node;

                visited.Add(node);
                foreach (var neighbor in node.Child)
                    if (!visited.Contains(neighbor) && neighbor != null)
                        queue.Enqueue(neighbor);
            }

            throw new InvalidOperationException();
        }

        public Node FindNode(string address)
        {
            var node = Root.ElementAt(0);
            foreach (var c in address)
                switch (c)
                {
                    case 'l':

                        node = node.LeftChild;
                        break;
                    case 'r':

                        node = node.RightChild;

                        break;
                    default:
                        throw new Exception();
                }

            return node;
        }

        public void CreateRoot(string data)
        {
            if (Root.Count == 0)
            {
                Root.Insert(0, new Node(_amountOfNodes, null, data));
                _amountOfNodes = 1;
            }
            else
            {
                throw new Exception("Root already present !");
            }
        }

        public int DeleteNode(int uniqNumber)
        {
            var child = FindNode(uniqNumber);


            if (child.LeftChild != null)
                DeleteNode(child.LeftChild.UniqNumber);
            if (child.RightChild != null)
                DeleteNode(child.RightChild.UniqNumber);


            var parentNode = child.Parent;
            if (parentNode != null)
            {
                if (parentNode.LeftChild != null && parentNode.LeftChild == child)
                    parentNode.LeftChild = null;
                else if (parentNode.RightChild != null && parentNode.RightChild == child)
                    parentNode.RightChild = null;
                _amountOfNodes--;
            }
            else
            {
                child.Data = null;
                Root.Clear();
                _amountOfNodes = 0;
            }

            return uniqNumber;
        }

        public string ShowData(int uniqNumber)
        {
            return FindNode(uniqNumber).Data;
        }

        private void MakeStorableString(Node root, ref string storable, string address = "")
        {
            if (root != null)
            {
                storable += root.Data + "*" + address + "\n";
                MakeStorableString(root.LeftChild, ref storable, address + "l");
                MakeStorableString(root.RightChild, ref storable, address + "r");
            }
        }

        public void StoreTreeToFile(List<Node> root, string filePath)
        {
            var storableString = string.Empty;
            if (root != null)
            {
                MakeStorableString(root.ElementAt(0), ref storableString);
                File.WriteAllText(filePath, storableString);
            }
        }

        private void MakeNodeFromAddress(string line)
        {
            var root = Root.ElementAt(0);
            var data = line.Substring(0, line.IndexOf('*'));
            var address = line.Substring(line.LastIndexOf('*') + 1);

            if (string.IsNullOrWhiteSpace(address))
            {
                root.Data = data;
            }
            else
            {
                var last = address.ElementAt(address.Length - 1);
                var toSearch = address.Substring(0, address.Length - 1);
                var node = FindNode(toSearch);
                AddNode(node, data, last == 'r');
            }
        }

        public Node LoadTreeFromFile(string filePath)
        {
            if (File.Exists(filePath) == false)
                throw new Exception("File does NOT exist!");

            var lines = File.ReadAllLines(filePath).ToList();

            foreach (var line in lines) MakeNodeFromAddress(line);

            return null;
        }
    }
}