using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace BinaryTree
{
    internal class ReadableTextFile
    {
        private readonly Node _root;
        private int _currentDepth;
        private readonly int _lengthOfData;
        private readonly List<string> _lines = new List<string>();

        public int DepthOfTree
        {
            get { return GetDepth(root); }
        }
        public int WidthOfTree
        {
            get { return (int)Math.Pow(2, DepthOfTree + 1) - 1; }
        }

        public int WidthOfTree => (int)Math.Pow(2, DepthOfTree) * 3 + (int)Math.Pow(2, DepthOfTree) - 1;


        public void StoreToFile(string filePath)
        {
            var text = string.Empty;

            foreach (var line in _lines)
            {
                Debug.WriteLine(line);
                text += line + '\n';
            }


            File.WriteAllText(filePath, text);
        }

        private void PrepareLines()
        {
            for (var i = 0; i < WidthOfTree; i++)
            {
                _lines.Add(string.Empty);
            }

        }

        private int getNumberOfLine(Node node) // returns real position of node according to its parents
        {
            var defaultPosition = WidthOfTree / 2;
            var changeOfPosition = 0;
            

            while (node != _root)
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

        private void PrintTree(Node root)
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
                    _lines[i] += actualNode.Data.PadRight(_lengthOfData, '-');
                else
                    _lines[i] += new string(' ', _lengthOfData);
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
                    PrintBranch(numberOfLine, actualNode);

                if (unexploredNodes.Count > 0)
                    actualNode = unexploredNodes[0];

                widthOfText = (int)Math.Pow(2, DepthOfTree - actualNode.Depth) * 4 - 1;
*/
                numberOfLine = GetNumberOfLine(actualNode);

            }
            while (unexploredNodes.Count != 0);
        }

        public ReadableTextFile(Node root, int lengthOfData)
        {
            _root = root;
            _lengthOfData = lengthOfData;
            _currentDepth = 0;
            PrepareLines();
            PrintTree(root);
        }

        
        private void ExploreTree(Node node, ref List<int> elements)
        {
            if (elements.Count <= _currentDepth)
                elements.Add(1);
            else
                elements[_currentDepth]++;

            if (node.LeftChild != null)
            {
                _currentDepth++;
                ExploreTree(node.LeftChild, ref elements);
            }
            if (node.RightChild != null)
            {
                _currentDepth++;
                ExploreTree(node.RightChild, ref elements);
            }
            _currentDepth--;
        }

        private int GetDepth(Node root)
        {
            _currentDepth = 0;
            var elements = new List<int>();
            ExploreTree(root, ref elements);
            return elements.Count - 1;
        }



        private void printBranch(int numberOfLine, Node root)   // print whole branch, including empty space
        {
            int widthOfText = (int)Math.Pow(2, DepthOfTree - root.Depth - 1) * 4 - 1;
            int border = (int)Math.Pow(2, DepthOfTree - root.Depth - 1);

            for (var i = 0; i < widthOfText / 2 + 1; i++)
            {   
                if (i == 0)
                {
                    _lines[numberOfLine] += "|  " + new string(' ', _lengthOfData);
                }
                else if ( i > border)
                {
                    _lines[numberOfLine + i] += "   " + new string(' ', _lengthOfData);
                    _lines[numberOfLine - i] += "   " + new string(' ', _lengthOfData);
                }
                else if (i == border) 
                {
                    _lines[numberOfLine + i] += "|--";
                    _lines[numberOfLine - i] += "|--";

                    if (rootNode.RightChild != null)
                        _lines[numberOfLine - i] += rootNode.RightChild.Data.PadRight(_lengthOfData, '-');
                    else
                        _lines[numberOfLine - i] += "";

                    if (rootNode.LeftChild != null)
                        _lines[numberOfLine + i] += rootNode.LeftChild.Data.PadRight(_lengthOfData, '-');
                    else
                        _lines[numberOfLine + i] += "";
                }
                else
                {
                    _lines[numberOfLine + i] += "|  " + new string(' ', _lengthOfData);
                    _lines[numberOfLine - i] += "|  " + new string(' ', _lengthOfData);
                }
            }
        }

    }
}
