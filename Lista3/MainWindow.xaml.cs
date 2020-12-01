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
        public static int Index;
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

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {



            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.DefaultExt = "xml";
            //saveFileDialog.Filter = "XML-File | *.xml";
            //saveFileDialog.FilterIndex = 1;

            //if (saveFileDialog.ShowDialog() == true)
            //{
            //    string FilePath = saveFileDialog.FileName;

            //    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<StudentCheker>));
            //    using (TextWriter tw = new StreamWriter($"{FilePath}"))
            //    {
            //        serializer.Serialize(tw, Students);
            //    }
            //}


        }






        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "XML-File | *.xml";
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    string FilePath = openFileDialog.FileName;
            //    XmlSerializer xmldeserializer = new XmlSerializer(typeof(ObservableCollection<StudentCheker>));
            //    using (TextReader reader = new StreamReader(FilePath))
            //    {

                    
            //        object FileInfo = xmldeserializer.Deserialize(reader);
            //        Students = (ObservableCollection<StudentCheker>)FileInfo;

            //    }

            //    Data.ItemsSource = null;
            //    Data.ItemsSource = Students;

            //}

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {




            InsertWindow insertWindow = new InsertWindow();
            insertWindow.Show();
            insertWindow.Closing += InsertWindow_Closing;

        }

        private void InsertWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Data.ItemsSource = null;

            Data.ItemsSource = Students;

        }

    

     

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            Data.ItemsSource = null;

            Data.ItemsSource = Students;

        }

        public void Refresh()
        {

            Data.ItemsSource = null;

            Data.ItemsSource = Students;

        }



        private void Data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var x = (Student)Data.Items[Data.SelectedIndex];
            Index = Students.ToList<Student>().FindIndex(item => item.id_stud == x.id_stud);
            OutputWindow outputWindow = new OutputWindow();
            outputWindow.Show();
            outputWindow.Closing += OutputWindow_Closing;


        }

        private void OutputWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Data.ItemsSource = null;

            Data.ItemsSource = Students;
        }


        private void MenuItem_Save()
        {



            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.Filter = "XML-File | *.xml";
            saveFileDialog.FilterIndex = 1;

            if (saveFileDialog.ShowDialog() == true)
            {
                string FilePath = saveFileDialog.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof(List<StudentCheker>));
                using (TextWriter tw = new StreamWriter($"{FilePath}"))
                {
                    serializer.Serialize(tw, Students);
                }

                
            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Students.Count!=0)
            {


                MessageBoxResult result = MessageBox.Show("Czy chcesz zapisać swoje zmiany?", "Uwaga!", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        e.Cancel = true;
                        MenuItem_Save();
                        e.Cancel = false;
                        break;
                    case MessageBoxResult.No:

                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;

                }
            }
        }




        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            if (Students.Count != 0)
            {


                MessageBoxResult result = MessageBox.Show("Czy chcesz zapisać swoje zmiany?", "Uwaga!", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MenuItem_Save();
                        break;
                    case MessageBoxResult.No:

                        break;
                    case MessageBoxResult.Cancel:
                        return;
                        


                }
            }
            Students.Clear();
            Data.ItemsSource = null;
            Data.ItemsSource = Students;



        }
    }
}
