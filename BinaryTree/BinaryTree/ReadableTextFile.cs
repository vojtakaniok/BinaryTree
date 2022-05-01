using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BinaryTree
{
    class ReadableTextFile
    {
        private readonly Node root;
        int currentDepth, lengthOfData;
        private List<string> lines = new List<string>();

        public int DepthOfTree
        {
            get { return GetDepth(root); }
        }
        public int WidthOfTree
        {
            get { return (int)Math.Pow(2, DepthOfTree + 1) - 1; }
        }


        public void StoreToFile(string filePath)
        {
            string text = string.Empty;

            foreach (var line in lines)
            {
                Debug.WriteLine(line);
                text += line + '\n';
            }


            File.WriteAllText(filePath, text);
        }

        private void prepareLines()
        {
            for (int i = 0; i < WidthOfTree; i++)
            {
                lines.Add(string.Empty);
            }

        }

        private int getNumberOfLine(Node node) // returns real position of node according to its parents
        {
            int defaultPosition = WidthOfTree / 2;
            int changeOfPosition = 0;
            

            while (node != root)
            {
                int move = (int)Math.Pow(2, DepthOfTree - node.Depth );
                if (node.Parent.LeftChild == node)
                {
                    changeOfPosition += move;
                }
                else
                {
                    changeOfPosition -= move;
                }
                node = node.Parent;
            }
            return defaultPosition + changeOfPosition;
            
        }

        private void printTree(Node root)
        {
            if (root == null)
                throw new Exception("There is nothing to print!");

            int widthOfText = this.WidthOfTree;
            List<Node> unexploredNodes = new List<Node>();
            Node actualNode = root;
            int numberOfLine = widthOfText / 2;

            for (int i = 0; i < widthOfText; i++) // print root
            {
                if (i == numberOfLine)
                    lines[i] += actualNode.data.PadRight(lengthOfData, '-');
                else
                    lines[i] += new string(' ', lengthOfData);
            }

            do   // BFS algorithm, from rightchild to leftchild
            {   
                if (unexploredNodes.Count != 0)
                    unexploredNodes.Remove(unexploredNodes[0]);
                if (actualNode.RightChild != null)
                {
                    unexploredNodes.Add(actualNode.RightChild);
                }

                if (actualNode.LeftChild != null)
                {
                    unexploredNodes.Add(actualNode.LeftChild);
                }
                if (actualNode.LeftChild != null | actualNode.RightChild != null)
                    printBranch(numberOfLine, actualNode);

                if (unexploredNodes.Count > 0)
                    actualNode = unexploredNodes[0];

                widthOfText = (int)Math.Pow(2, DepthOfTree - actualNode.Depth) * 4 - 1;
                numberOfLine = getNumberOfLine(actualNode);

            }
            while (unexploredNodes.Count != 0);
        }

        public ReadableTextFile(Node root, int lengthOfData)
        {
            this.root = root;
            this.lengthOfData = lengthOfData;
            this.currentDepth = 0;
            prepareLines();
            printTree(root);
        }

        
        private void ExploreTree(Node node, ref List<int> elements)
        {
            if (elements.Count <= currentDepth)
                elements.Add(1);
            else
                elements[currentDepth]++;

            if (node.LeftChild != null)
            {
                currentDepth++;
                ExploreTree(node.LeftChild, ref elements);
            }
            if (node.RightChild != null)
            {
                currentDepth++;
                ExploreTree(node.RightChild, ref elements);
            }
            currentDepth--;
            return;
        }

        private int GetDepth(Node root)
        {
            currentDepth = 0;
            List<int> elements = new List<int>();
            ExploreTree(root, ref elements);
            return elements.Count - 1;
        }



        private void printBranch(int numberOfLine, Node root)   // print whole branch, including empty space
        {
            int widthOfText = (int)Math.Pow(2, DepthOfTree - root.Depth - 1) * 4 - 1;
            int border = (int)Math.Pow(2, DepthOfTree - root.Depth - 1);

            for (int i = 0; i < widthOfText / 2 + 1; i++)
            {   
                if (i == 0)
                {
                    lines[numberOfLine] += "|  " + new string(' ', lengthOfData);
                }
                else if ( i > border)
                {
                    lines[numberOfLine + i] += "   " + new string(' ', lengthOfData);
                    lines[numberOfLine - i] += "   " + new string(' ', lengthOfData);
                }
                else if (i == border) 
                {
                    lines[numberOfLine + i] += "|--";
                    lines[numberOfLine - i] += "|--";

                    if (root.RightChild != null)
                        lines[numberOfLine - i] += root.RightChild.data.PadRight(lengthOfData, '-');
                    else
                        lines[numberOfLine - i] += "NULL";

                    if (root.LeftChild != null)
                        lines[numberOfLine + i] += root.LeftChild.data.PadRight(lengthOfData, '-');
                    else
                        lines[numberOfLine + i] += "NULL";
                }
                else
                {
                    lines[numberOfLine + i] += "|  " + new string(' ', lengthOfData);
                    lines[numberOfLine - i] += "|  " + new string(' ', lengthOfData);
                }
            }
        }

    }
}
