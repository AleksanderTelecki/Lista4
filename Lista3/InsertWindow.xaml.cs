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


        Student student = new Student();
        List<TextBox> textBoxes = new List<TextBox>();
        public InsertWindow()
        {
            InitializeComponent();
            textBoxes.Add(_Name);
            textBoxes.Add(_SurName);
            textBoxes.Add(_City);
            textBoxes.Add(_Age);
            textBoxes.Add(_Pesel);
            textBoxes.Add(_Adress);
            textBoxes.Add(_NrAlbumu);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                student.PersonStringImage = Student.ImgToStr(saveFileDialog.FileName);
                ImageFrame.Source = student.PersonImage;  
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            try
            {



                if (CheckText())
                {


                    student.Name = _Name.Text;
                    student.SurName = _SurName.Text;
                    student.HomeCity = _City.Text;
                    student.Pesel = _Pesel.Text;
                    student.Age = int.Parse(_Age.Text);
                    student.Adress = _Adress.Text;
                    student.NumerAlbumu = _NrAlbumu.Text;
                    student.DateOfBirth = _Date.SelectedDate.Value;
                    student.SetId();
                    MainWindow.Students.Add(student);
                    MessageBox.Show("Success!");
                    ClearFields();


                }
            }
            catch (Exception)
            {
                MessageBox.Show("Field:<Wiek> - Incorect Data");
                
            }




        }


        private void ClearFields()
        {
            foreach (var item in textBoxes)
            {
                item.Text = string.Empty;
                
            }
            ImageFrame.Source = null;
            _Date.SelectedDate = null;
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
