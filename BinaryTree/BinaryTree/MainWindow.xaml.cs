using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace BinaryTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var newtree = new Tree();
            var path = @"binTree.txt";
            Debug.WriteLine(Directory.GetCurrentDirectory());

            newtree.LoadTreeFromFile(path);
            newtree.StoreTreeToFile(newtree.Root, path);

            ReadableTextFile sw = new ReadableTextFile(newtree.Root);
            Debug.WriteLine("Depth: " + sw.DepthOfTree + "\nWidth: " + sw.WidthOfTree);
            sw.StoreToFile(@"Readable.txt");
            for (int i = 0; i < 5; i++)
                Console.WriteLine((int)Math.Pow(2, i) * 3 + (int)Math.Pow(2, i) - 1);
        }
    }
}