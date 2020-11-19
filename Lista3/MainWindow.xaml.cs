﻿using System;
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

namespace Lista3
{

    public partial class MainWindow : Window
    {

        public static List<Person> Persons = new List<Person>();
        public static int Index;
        public MainWindow()
        {
            InitializeComponent();

            //Persons.Add(new Person("Zbigniew", "Szwecki", "sda2313asda", "Kraków", DateTime.Now, 19));
            //Persons.Add(new Person("Adam", "Kowalski", "asdad12313","Opole",DateTime.Now,12));
            //Persons.Add(new Person("Olek", "Telecki", "sda2313413da", "Rzeszów", DateTime.Now, 26));

            Data.ItemsSource = Persons;

          
            
            
        }

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {



            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.Filter = "XML-File | *.xml";
            saveFileDialog.FilterIndex = 1;

            if (saveFileDialog.ShowDialog() == true)
            {
                string FilePath = saveFileDialog.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                using (TextWriter tw = new StreamWriter($"{FilePath}"))
                {
                    serializer.Serialize(tw, Persons);
                }
            }


        }






        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML-File | *.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                string FilePath = openFileDialog.FileName;
                XmlSerializer xmldeserializer = new XmlSerializer(typeof(List<Person>));
                using (TextReader reader = new StreamReader(FilePath))
                {

                    
                    object FileInfo = xmldeserializer.Deserialize(reader);
                    Persons = (List<Person>)FileInfo;

                }

                Data.ItemsSource = null;
                Data.ItemsSource = Persons;

            }

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

            Data.ItemsSource = Persons;

        }

    

     

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            Data.ItemsSource = null;

            Data.ItemsSource = Persons;

        }

    

        private void Data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var x = (Person)Data.Items[Data.SelectedIndex];
            Index = Persons.FindIndex(item => item.ID == x.ID);
            OutputWindow outputWindow = new OutputWindow();
            outputWindow.Show();
            outputWindow.Closing += OutputWindow_Closing;


        }

        private void OutputWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Data.ItemsSource = null;

            Data.ItemsSource = Persons;
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

                XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                using (TextWriter tw = new StreamWriter($"{FilePath}"))
                {
                    serializer.Serialize(tw, Persons);
                }

                
            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Persons.Count!=0)
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
            if (Persons.Count != 0)
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
            Persons.Clear();
            Data.ItemsSource = null;
            Data.ItemsSource = Persons;



        }
    }
}
