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
           
            Debug.WriteLine(Directory.GetCurrentDirectory());

            var path = @"binTree.txt";
            _newTree.LoadTreeFromFile(path);
            BinaryTree.ItemsSource = _newTree.Root;
            

            
            

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var node = BinaryTree.SelectedItem as Node;
            _newTree.DeleteNode(node.UniqNumber);
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrWhiteSpace(Data.Text))
                return;
            try
            {
                var node = BinaryTree.SelectedItem as Node;
            
                _newTree.AddNode(node, Data.Text);
            }
            catch (Exception ex)
            {
               CreateDialog("Cannot add Left node, Reason:" + ex.Message);
            }
        }

        private void CreateDialog(string text)
        {
            string txt = "Error";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxResult result = MessageBox.Show(text, txt, button);
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Data.Text))
                return;
            try
            {
                var node = BinaryTree.SelectedItem as Node;
                _newTree.AddNode(node, Data.Text, true);
            }
            catch (Exception ex)
            {
                CreateDialog("Cannot add Right node, Reason:"+ ex.Message);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var path = @"binTree.txt";
            _newTree.StoreTreeToFile(_newTree.Root, path);
        }

        private void SaveReadable_Click(object sender, RoutedEventArgs e)
        {
            var sw = new ReadableTextFile(_newTree.Root, _newTree.MaxLength);
            sw.StoreToFile(@"Readable.txt");
            Debug.WriteLine("Depth: " + sw.DepthOfTree + "\nWidth: " + sw.WidthOfTree);
        }
    }
}