using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace BinaryTree
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var newTree = new Tree();
            var path = @"binTree.txt";
            Debug.WriteLine(Directory.GetCurrentDirectory());

            newTree.LoadTreeFromFile(path);
            newTree.StoreTreeToFile(newTree.Root, path);

            var sw = new ReadableTextFile(newTree.Root, newTree.MaxLength);
            Debug.WriteLine("Depth: " + sw.DepthOfTree + "\nWidth: " + sw.WidthOfTree);
            sw.StoreToFile(@"Readable.txt");
            for (var i = 0; i < 5; i++)
                Console.WriteLine((int) Math.Pow(2, i) * 3 + (int) Math.Pow(2, i) - 1);
        }
    }
}