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
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        MCollection<Student> MainCollection = (MCollection<Student>)Application.Current.Properties["MyColl"];
        public AddPage()
        {
            InitializeComponent();
            Update();          
        }
        private void Update()
        {
            PositionInCollection.Items.Clear();

            PositionInCollection.Items.Add("First");
            PositionInCollection.Items.Add("Last");

            for (int i = 0; i < MainCollection.Count; i++)
                PositionInCollection.Items.Add(i.ToString());

            ArmyPoint.IsChecked = false;
            PositionInCollection.SelectedItem = null;
            Course.SelectedItem = null;
            NameOfStudent.SelectedItem = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ErroMessage.Visibility = Visibility.Hidden;
            try
            {
                Student NewStudent = new Student();
                NewStudent.Army = ArmyPoint.IsChecked.Value;
                NewStudent.Course = Convert.ToInt32(((ListBoxItem)Course.SelectedItem).Content);
                NewStudent.Name = Student.ConvertToNames((String)(((ListBoxItem)NameOfStudent.SelectedItem).Content));
                String Position =(String)PositionInCollection.SelectedItem;
                switch (Position)
                {
                    case "First":
                        MainCollection.AddToBegin(NewStudent);
                        break;
                    case "Last":
                        MainCollection.AddToEnd(NewStudent);
                        break;
                    default:
                        MainCollection.Add(Convert.ToInt32(Position), NewStudent);
                        break;
                }
                Update();
            }
            catch
            {
                ErroMessage.Visibility = Visibility.Visible;
            }
            
        }         
    }
}
