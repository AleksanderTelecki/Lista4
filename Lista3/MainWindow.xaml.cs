using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using System.Windows.Markup;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Lista3
{

    public partial class MainWindow : Window
    {

        public static ObservableCollection<Student> Students = new ObservableCollection<Student>();
        
        public static string IndexID;
        public MainWindow()
        {
           
            InitializeComponent();
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            StudentsEntities entities = new StudentsEntities();
            foreach (var item in entities.Student.ToList<Student>())
            {
                Students.Add(item);
            }
            
            
            Data.ItemsSource = Students.ToList<Student>();
      
            Students.CollectionChanged += Students_CollectionChanged;
            
          
            
            
        }

        private void Students_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Refresh();
        }

     






      

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            InsertWindow insertWindow = new InsertWindow();
            insertWindow.Show();
            insertWindow.Closing += InsertWindow_Closing;

        }

        private void InsertWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Refresh();

        }

    

     

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            Refresh();

        }

        public void Refresh()
        {
            StudentsEntities entities = new StudentsEntities();
            Data.ItemsSource = null;
            Students = new ObservableCollection<Student>();
            foreach (var item in entities.Student)
            {
                Students.Add(item);
            }
            Data.ItemsSource = Students;

        }



        private void Data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Data.SelectedIndex!=-1)
            {


                var x = (Student)Data.Items[Data.SelectedIndex];
                IndexID = x.id_stud;
                OutputWindow outputWindow = new OutputWindow();
                outputWindow.Show();
                outputWindow.Closing += OutputWindow_Closing;
            }

        }

        private void OutputWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Refresh();
        }


      




        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }




     
    }
}
