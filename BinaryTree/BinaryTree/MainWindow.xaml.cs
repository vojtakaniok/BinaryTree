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
        private readonly Tree _newTree;

        public MainWindow()
        {
            InitializeComponent();
            _newTree = new Tree();
            var path = @"binTree.txt";
            Debug.WriteLine(Directory.GetCurrentDirectory());


            _newTree.LoadTreeFromFile(path);
            BinaryTree.ItemsSource = _newTree.Root;
            _newTree.StoreTreeToFile(_newTree.Root, path);

            var sw = new ReadableTextFile(_newTree.Root, _newTree.MaxLength);
            Debug.WriteLine("Depth: " + sw.DepthOfTree + "\nWidth: " + sw.WidthOfTree);
            sw.StoreToFile(@"Readable.txt");
            for (var i = 0; i < 5; i++)
                Console.WriteLine((int) Math.Pow(2, i) * 3 + (int) Math.Pow(2, i) - 1);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var node = BinaryTree.SelectedItem as Node;
            _newTree.DeleteNode(node.UniqNumber);
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            var node = BinaryTree.SelectedItem as Node;
            _newTree.AddNode(node, Data.Text);
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            var node = BinaryTree.SelectedItem as Node;
            _newTree.AddNode(node, Data.Text, true);
        }
    }
}