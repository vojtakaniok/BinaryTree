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
            Root = new Node(_amountOfNodes, null, data);
            _amountOfNodes++;
        }

        public Tree()
        {
            _amountOfNodes = 0;
            Root = new Node(_amountOfNodes, null);
            _amountOfNodes++;
        }

        public Node Root { set; get; }
        public int MaxLength { get; private set; }

        public int AddNode(Node node, string data, bool startWithRight = false)
        {
            if (data.Contains("*") ^ data.Contains(";"))
                throw new Exception("Data cannot contain '*' or ';' character!");
            if (MaxLength < data.Length)
                MaxLength = data.Length;

            if ((node.LeftChild == null) & (startWithRight == false))
            {
                node.LeftChild = new Node(_amountOfNodes, node, data);
                _amountOfNodes++;
                return _amountOfNodes - 1;
            }

            if (node.RightChild == null)
            {
                node.RightChild = new Node(_amountOfNodes, node, data);
                _amountOfNodes++;
                return _amountOfNodes - 1;
            }

            throw new Exception("Node can't have more than two children!");
        }

        public Node FindNode(int uniqNumber)
        {
            var node = Root;
            var list = new List<Node>(); // list of unexplored nodes.
            var smthToExplore = false;

            do
            {
                if (node.UniqNumber == uniqNumber)
                    return node;
                if (list.Count > 0)
                    smthToExplore = true;

                if (node.LeftChild != null) // If there's left child, jump into it. Save RightChild, if possible
                {
                    if (node.RightChild != null)
                        list.Add(node.RightChild);
                    node = node.LeftChild;
                }
                else // If node has no Left child, save Right child (if possible) and jump into some unexplored nodes.
                {
                    if (node.RightChild != null)
                        list.Add(node.RightChild);
                    node = list[0]; // jump
                    list.Remove(list[0]); //remove exploring node from the list
                }
            } while (!((node.RightChild == null) & (node.LeftChild == null) & (smthToExplore == false)));

            throw new Exception("There is no node with that UniqNumber!");
        }

        public int DeleteNode(int uniqNumber)
        {
            var child = FindNode(uniqNumber);
            var parentNode = child.Parent;
            if (parentNode.LeftChild == child)
                parentNode.LeftChild = null;
            else
                parentNode.RightChild = null;
            _amountOfNodes--;
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
                storable += root.Data + "*" + address + ";\n";
                MakeStorableString(root.LeftChild, ref storable, address + "l");
                MakeStorableString(root.RightChild, ref storable, address + "r");
            }
        }

        public void StoreTreeToFile(Node root, string filePath)
        {
            var storableString = string.Empty;
            if (root != null)
            {
                MakeStorableString(root, ref storableString);
                File.WriteAllText(filePath, storableString);
            }
        }

        private void MakeNodeFromAddress(string line)
        {
            var root = Root;
            var data = line.Substring(0, line.IndexOf('*'));
            var address = line.Substring(line.LastIndexOf('*') + 1);
            var i = 1;
            var searching = true;
            if (string.IsNullOrWhiteSpace(address))
                root.Data = data;

            foreach (var c in address)
            {
                if (i == address.Length) searching = false;
                i++;
                switch (c)
                {
                    case 'l':
                        if (searching)
                        {
                            root = root.LeftChild;
                            break;
                        }

                        AddNode(root, data);
                        break;
                    case 'r':
                        if (searching)
                        {
                            root = root.RightChild;
                            break;
                        }

                        AddNode(root, data, true);
                        break;
                    default:
                        throw new Exception();
                }
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