using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace BinaryTree
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tree _newTree;

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
            if (string.IsNullOrWhiteSpace(Data.Text))
                return;
            try
            {
                var node = BinaryTree.SelectedItem as Node;

                _newTree.AddNode(node, Data.Text);
            }
            catch (Exception ex)
            {
                CreateDialog("Cannot add Left node, Reason: " + ex.Message);
            }
        }

        private void CreateDialog(string text)
        {
            var txt = "Error";
            var button = MessageBoxButton.OK;
            var result = MessageBox.Show(text, txt, button);
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Data.Text))
                return;
            try
            {
                var node = BinaryTree.SelectedItem as Node;
                _newTree.AddNode(node, Data.Text, true);
            }
            catch (Exception ex)
            {
                CreateDialog("Cannot add Right node, Reason: " + ex.Message);
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

        private void God_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Data.Text))
                return;
            try
            {
                _newTree.CreateRoot(Data.Text);
                BinaryTree.ItemsSource = _newTree.Root;
                BinaryTree.Items.Refresh();
            }
            catch (Exception ex)
            {
                CreateDialog("Cannot add Right node, Reason: " + ex.Message);
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Path.Text))
                return;
            try
            {
                _newTree = new Tree();
                _newTree.LoadTreeFromFile(Path.Text);
                BinaryTree.ItemsSource = _newTree.Root;
                BinaryTree.Items.Refresh();
            }
            catch (Exception ex)
            {
                CreateDialog("Cannot load file, Reason: "+ ex.Message);
            }
        }

        private void Dialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                Path.Text = openFileDialog.FileName;
        }
    }
}