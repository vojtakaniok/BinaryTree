using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BinaryTree
{
    class Tree
    {
        public Node Root { set; get; }
        private int AmountOfNodes;
        
        public Tree(string data)
        {
            AmountOfNodes = 0;
            Root = new Node(AmountOfNodes, null, data);
            AmountOfNodes++;
        }

        public Tree()
        {
            AmountOfNodes = 0;
            Root = new Node(AmountOfNodes, null);
            AmountOfNodes++;
        }

        public int AddNode(Node node, string data, bool startWithRight = false)
        {
            if (data.ToString().Contains("*") ^ data.ToString().Contains(";"))
                throw new Exception("Data cannot contain '*' or ';' character!");

            if (node.LeftChild == null & startWithRight == false)
            {
                node.LeftChild = new Node(AmountOfNodes, node, data);
                AmountOfNodes++;
                return AmountOfNodes - 1;
            }
            else if (node.RightChild == null)
            {
                node.RightChild = new Node(AmountOfNodes, node, data);
                AmountOfNodes++;
                return AmountOfNodes - 1;
            }
            else
                throw new Exception("Node can't have more than two children!");
        }

        public Node FindNode(int uniqNumber)
        {
            Node node = Root;
            List<Node> list = new List<Node>();   // list of unexplored nodes.
            bool SmthToExplore = false;

            do
            {
                if (node.UniqNumber == uniqNumber)
                    return node;
                if (list.Count > 0)
                    SmthToExplore = true;

                if (node.LeftChild != null)     // If there's left child, jump into it. Save RightChild, if possible
                {
                    if (node.RightChild != null)
                        list.Add(node.RightChild);
                    node = node.LeftChild;
                }
                else   // If node has no Left child, save Right child (if possible) and jump into some unexplored nodes.
                {
                    if (node.RightChild != null)
                        list.Add(node.RightChild);
                    node = list[0]; // jump
                    list.Remove(list[0]); //remove exploring node from the list
                }

            } while (!(node.RightChild == null & node.LeftChild == null & SmthToExplore == false));

            throw new Exception("There is no node with that UniqNumber!");
        }

        public int DeleteNode(int uniqNumber)
        {
            Node parentNode = FindNode(uniqNumber);
            Node child = parentNode;
            parentNode = parentNode.Parent;
            if (parentNode.LeftChild == child)
            {
                parentNode.LeftChild = null;
            }
            else
                parentNode.RightChild = null;
            AmountOfNodes--;
            return uniqNumber;
        }

        public string ShowData(int uniqNumber)
        {
            return FindNode(uniqNumber).data;
        }

        private void MakeStoreableString(Node root, ref string storeable, string address = "")
        {
            if (root == null)
            {   
                return;
            }    
            
            else
            {
                storeable += root.data.ToString() + "*" + address + ";\n";
                MakeStoreableString(root.LeftChild, ref storeable, address + "l");
                MakeStoreableString(root.RightChild, ref storeable, address + "r");
            }
        }

        public void StoreTreeToFile(Node root, string filePath)
        {
            string storeableString = string.Empty;
            MakeStoreableString(root, ref storeableString);
            File.WriteAllText(filePath, storeableString);
        }

        private void MakeNodeFromAdress(string line)
        {
            bool readingData = true;
            bool nodeCreated = false;
            Node root = Root;
            StringBuilder data = new StringBuilder();

            foreach (char c in line) 
            {
                if (c == '*' & readingData == true) //first of all, read data
                {
                    readingData = false;
                }
                else if (readingData == true)
                {
                    data.Append(c);
                }
                else if (c == '*' & readingData == false) //if there are two '*' chars, smth's wrong
                {
                    throw new Exception("File was not build correctly, cannot make node from it!");
                }   
                else    //Then manage making nodes
                {   
                    try
                    {
                        if (c == 'l' & nodeCreated == false)
                        {
                            if (root.LeftChild == null)
                            {
                                AddNode(root, data.ToString());
                                nodeCreated = true;
                                continue;
                            }
                            root = root.LeftChild;
                        }
                        else if (c == 'r' & nodeCreated == false)
                        {
                            if (root.RightChild == null)
                            {
                                AddNode(root, data.ToString(), true);
                                nodeCreated = true;
                                continue;
                            }
                            root = root.RightChild;
                        }
                        else if (c == ';' & nodeCreated == false)
                        {
                            if (root != null)
                            {
                                root.data = data.ToString();
                            }
                        }
                        else if (c == ';' & nodeCreated == true)
                        {
                            break;
                        }
                        else
                            throw new Exception("Attempt to create two nodes from one adress!");
                    }
                    catch   //if AddNode creates exception due to more than 2 childs
                    {
                        throw new Exception("File was not built correctly and cannot be converted to tree!");
                    }     
                }  
            }
        }

        public Node LoadTreeFromFile(string filePath)
        {
            if (File.Exists(filePath) == false)
                throw new Exception("File does NOT exist!");

            List<string> lines = File.ReadAllLines(filePath).ToList();
            
            foreach (string line in lines)
            {
                MakeNodeFromAdress(line);
            }

            return null;
        }

    }
}
