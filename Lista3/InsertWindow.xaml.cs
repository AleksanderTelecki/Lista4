using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;

namespace Lista3
{
    /// <summary>
    /// Interaction logic for InsertWindow.xaml
    /// </summary>
    public partial class InsertWindow : Window
    {


        Person person = new Person();
        List<TextBox> textBoxes = new List<TextBox>();
        public InsertWindow()
        {
            InitializeComponent();
            textBoxes.Add(_Name);
            textBoxes.Add(_SurName);
            textBoxes.Add(_City);
            textBoxes.Add(_Age);
            textBoxes.Add(_Pesel);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                person.PersonStringImage = Person.ImgToStr(saveFileDialog.FileName);
                ImageFrame.Source = person.PersonImage;  
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            try
            {



                if (CheckText())
                {


                    person.Name = _Name.Text;
                    person.SurName = _SurName.Text;
                    person.HomeCity = _City.Text;
                    person.Pesel = _Pesel.Text;
                    person.Age = int.Parse(_Age.Text);
                    person.DateOfBirth = _Date.SelectedDate.Value;
                    person.SetId();
                    MainWindow.Persons.Add(person);
                    MessageBox.Show("Success!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Field:<Wiek> - Incorect Data");
                
            }




        }


        private bool CheckText()
        {

            bool checker = true;
            foreach (var item in textBoxes)
            {
                if (String.IsNullOrEmpty(item.Text))
                {
                    item.Background = Brushes.Red;
                    checker=false;
                }
                else
                {
                    item.Background = Brushes.White;
                }

            }

            return checker;
        }
    }
}
