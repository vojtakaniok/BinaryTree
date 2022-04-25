using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BinaryTree
{
    class ReadableTextFile      // TO DO
    {
        private readonly Node root;

        private List<string> lines = new List<string>();
        int currentDepth, lengthOfData;
        private List<List<string>> matrix = new List<List<string>>();

        public int DepthOfTree
        {
            get { return GetDepth(root); }
        }
        public int WidthOfTree
        {
            get { return (int)Math.Pow(2, GetDepth(root)); }
        }

        private void createMatrix()
        {
            int amountOfLines = WidthOfTree - 1;

            for (int i = 0; i < amountOfLines; i++)
            {
                matrix.Add(new List<string>());
            }
        }

        public ReadableTextFile(Node root, int lengthOfData = 8)
        {
            this.root = root;
            this.lengthOfData = lengthOfData;
            this.currentDepth = 0;
            createMatrix();
        }

        private void printNodes(int currentDepth)
        {
            if (currentDepth == 0)
            {

            }
            else
            {

            }

            for (int i = 0; i < matrix.Count; i++)
            {   
                if (i == ) //there's a node to draw
                {

                }
                else
                {
                    matrix[i].Add("        ");
                }
                
            }
        }
        
        private void ExploreTree(Node node, ref List<int> elements) // goes from 
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
            return elements.Count;
        }

    }
}
