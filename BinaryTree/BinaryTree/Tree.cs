using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BinaryTree
{
    internal class Tree
    {
        public Node Root { set; get; }
        private int _amountOfNodes;
        private int _maxLeght = 0;
        public int MaxLeght
        {
            get { return _maxLeght; }
        }

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

        public int AddNode(Node node, string data, bool startWithRight = false)
        {
            if (data.Contains("*") ^ data.Contains(";"))
                throw new Exception("Data cannot contain '*' or ';' character!");
            if(_maxLeght < data.Length)
                _maxLeght = data.Length;

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
            var readingData = true;
            var nodeCreated = false;
            var root = Root;
            var data = new StringBuilder();

            foreach (var c in line)
                if ((c == '*') & readingData) //first of all, read data
                    readingData = false;
                else if (readingData)
                    data.Append(c);
                else if (c == '*') //if there are two '*' chars, smth's wrong
                    throw new Exception("File was not build correctly, cannot make node from it!");
                else //Then manage making nodes
                    try
                    {
                        if ((c == 'l') & (nodeCreated == false))
                        {
                            if (root != null && root.LeftChild == null)
                            {
                                AddNode(root, data.ToString());
                                nodeCreated = true;
                                continue;
                            }

                            root = root?.LeftChild;
                        }
                        else if ((c == 'r') & (nodeCreated == false))
                        {
                            if (root != null && root.RightChild == null)
                            {
                                AddNode(root, data.ToString(), true);
                                nodeCreated = true;
                                continue;
                            }

                            root = root?.RightChild;
                        }
                        else if ((c == ';') & (nodeCreated == false))
                        {
                            if (root != null) root.Data = data.ToString();
                        }
                        else if ((c == ';') & nodeCreated)
                        {
                            break;
                        }
                        else
                        {
                            throw new Exception("Attempt to create two nodes from one address!");
                        }
                    }
                    catch //if AddNode creates exception due to more than 2 child
                    {
                        throw new Exception("File was not built correctly and cannot be converted to tree!");
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