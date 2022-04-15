using System;
using System.Collections.Generic;
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
            var newtree = new Tree<string>("God");

            newtree.AddNode("Petr");
            newtree.AddNode("Pavel");
            newtree.AddNode(newtree.FirstNode.LeftChild,"Martin");
            newtree.AddNode(newtree.FirstNode.RightChild, "Jakub");
            newtree.AddNode(newtree.FirstNode.RightChild, "Iva");
            newtree.StoreToFile(newtree.FirstNode, "s");
        }
    }
}
