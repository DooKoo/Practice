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

namespace PracticApp.Pages
{
    /// <summary>
    /// Interaction logic for ShowPage.xaml
    /// </summary>
    public partial class ShowPage : Page
    {
        MCollection<Student> MainCollection = (MCollection<Student>)Application.Current.Properties["MyColl"];
        public ShowPage()
        {
            InitializeComponent();
            StudentsGrid.ItemsSource = MainCollection.ToArray();
            StudentsGrid.ColumnWidth = 169;
            StudentsGrid.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)// Need a test;
        {
            MCollection<Student> ExtraCollection= new MCollection<Student>();
            Student.Names NameOfSearch=default(Student.Names);
            int CourseOfSearch;
            String ArmyOfSearch;

            if (((ComboBoxItem)NameBox.SelectedItem).Content.ToString() != "All")
                NameOfSearch = Student.ConvertToNames(((ComboBoxItem)NameBox.SelectedItem).Content.ToString());
            else
                NameOfSearch = Student.Names.None;

            if (((ComboBoxItem)CourseBox.SelectedItem).Content.ToString() != "All")
                CourseOfSearch = Convert.ToInt32(((ComboBoxItem)CourseBox.SelectedItem).Content.ToString());
            else
                CourseOfSearch = 0;

            ArmyOfSearch = ((ComboBoxItem)ArmyBox.SelectedItem).Content.ToString();
            if (ArmyOfSearch == "Yes")
                ArmyOfSearch = "true";
            else if (ArmyOfSearch == "No")
                ArmyOfSearch = "false";
            if (NameOfSearch != Student.Names.None)
            {
                foreach (Student std in MainCollection)
                {

                    if (std.Name == NameOfSearch)
                    {
                        ExtraCollection.AddToEnd(std);
                    }

                }
            }
            else
                foreach(Student std in MainCollection)
                    ExtraCollection.AddToEnd(std);

            int i=0;
            if (CourseOfSearch != 0)
                foreach (Student std in ExtraCollection)
                {
                    if (std.Course != CourseOfSearch)
                        ExtraCollection.Delete(i);
                    else
                        i++;
                }
            i = 0;
            if (ArmyOfSearch != "All")
                foreach (Student std in ExtraCollection)
                {
                    if (std.Army != Convert.ToBoolean(ArmyOfSearch))
                        ExtraCollection.Delete(i);
                    else
                    i++;
                }
            StudentsGrid.ItemsSource = ExtraCollection.ToArray();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//[A..Z]
        {
            MainCollection.SortMin();
            StudentsGrid.ItemsSource = MainCollection.ToArray();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainCollection.SortMax();
            StudentsGrid.ItemsSource = MainCollection.ToArray();
        }
    }
}
