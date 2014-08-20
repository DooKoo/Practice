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
using Lib;

namespace PracticApp
{
    /// <summary>
    /// Interaction logic for DeletePage.xaml
    /// </summary>
    public partial class DeletePage : Page
    {
        MCollection<Student> MainCollection = (MCollection<Student>)Application.Current.Properties["MyColl"];
        public DeletePage()
        {
            InitializeComponent();
            Update();
        }
        /// <summary>
        /// Update data in the DataGrid.
        /// </summary>
        private void Update()
        {
            Numbers.ItemsSource = MainCollection.ToArray();
            Numbers.ColumnWidth = 162;
            Numbers.CanUserSortColumns = false;
            Numbers.IsReadOnly = true;
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Numbers.SelectedIndex == -1)
                    throw new Exception();

                MainCollection.Delete(Convert.ToInt32(Numbers.SelectedIndex));
                SolidColorBrush blue = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                ErrorMessage.Foreground = blue;
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Complete";
            }
            catch
            {
                SolidColorBrush red = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                ErrorMessage.Foreground = red;
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Error.Try again";
            }
            Update();
        }
    }
}
