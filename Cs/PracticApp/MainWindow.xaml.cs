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
using System.Threading;
using Lib;

namespace PracticApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.Properties["MyColl"] = MainCollection;
            }
            catch (Exception e)
            {
                var errorbox = MessageBox.Show("MCollection.dll not found", "Error");
                Thread.Sleep(300);
                Environment.Exit(0);
            }
        }
        public MCollection<Student> MainCollection = new MCollection<Student>();

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/AddPage.xaml", UriKind.Relative);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/DeletePage.xaml", UriKind.Relative);
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("Pages/ShowPage.xaml", UriKind.Relative);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
