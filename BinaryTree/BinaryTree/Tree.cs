using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Tree<T>
    {
        public Node<T> FirstNode { set; get; }
        private int AmountOfNodes;
        public Tree(T data)
        {
            AmountOfNodes = 0;
            FirstNode = new Node<T>(AmountOfNodes, null, data);
            AmountOfNodes++;
        }

        public Tree()
        {
            AmountOfNodes = 0;
            FirstNode = new Node<T>(AmountOfNodes, null);
            AmountOfNodes++;
        }

        public int AddNode(T data)
        {
            if (FirstNode.LeftChild == null)
            {
                FirstNode.LeftChild = new Node<T>(AmountOfNodes, FirstNode, data);
                AmountOfNodes++;
                return AmountOfNodes - 1;
            }
            else if (FirstNode.RightChild == null)
            {
                FirstNode.RightChild = new Node<T>(AmountOfNodes, FirstNode, data);
                AmountOfNodes++;
                return AmountOfNodes - 1;
            }
            else
                throw new Exception("Node can't have more than two children!");
        }

        public int AddNode(Node<T> node, T data)
        {
            if (node.LeftChild == null)
            {
                node.LeftChild = new Node<T>(AmountOfNodes, node, data);
                AmountOfNodes++;
                return AmountOfNodes - 1;
            }
            else if (node.RightChild == null)
            {
                node.RightChild = new Node<T>(AmountOfNodes, node, data);
                AmountOfNodes++;
                return AmountOfNodes - 1;
            }
            else
                throw new Exception("Node can't have more than two children!");
        }

        public Node<T> FindNode(int uniqNumber)
        {
            Node<T> node = FirstNode;
            List<Node<T>> list = new List<Node<T>>();   // list of unexplored nodes.
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
            Node<T> node1 = FindNode(uniqNumber);
            Node<T> node2 = node1;
            node1 = node1.Parent;
            if (node1.LeftChild == node2)
            {
                node1.LeftChild = null;
            }
            else
                node1.RightChild = null;
            AmountOfNodes--;
            return uniqNumber;
        }

        public T ShowData(int uniqNumber)
        {
            return FindNode(uniqNumber).data;
        }



    }
}
