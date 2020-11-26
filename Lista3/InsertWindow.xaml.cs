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
            _Date.SelectedDateChanged += _Date_SelectedDateChanged;
            DataStack.DataContext = new Student();
            _Date.ToolTip = "Pole nie może być puste";
            _Date.BorderBrush = Brushes.Red;

        }

        private void _Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_Date.SelectedDate!=null)
            {


                var Text = (DateTime.Today - _Date.SelectedDate).Value.Days.ToString();
                int age = (int.Parse(Text) / 365);
                if (age<18||age>90)
                {
                    _Date.ToolTip = "Wartość może być tylko w okresie od 18 lat do 90 lat";
                    _Date.BorderBrush = Brushes.Red;
                }
                else
                {
                    _Date.ToolTip = null;
                    _Date.BorderBrush = Brushes.White;
                }
                _Age.Text = age.ToString();

            }
            else
            {
                _Date.ToolTip = "Pole nie może być puste";
                _Date.BorderBrush = Brushes.Red;
            }
         

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
                    student.DateOfBirth = (DateTime)_Date.SelectedDate;
                    student.SetId();
                    MainWindow.Students.Add(student);
                    ClearFields();
                    

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Incorect Data");
                
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
            student = new Student();
        }


        private bool CheckText()
        {
            string ErrorBox = "";
            bool checker = true;
            foreach (var item in textBoxes)
            {
                if (item.ToolTip!=null)
                {
                    ErrorBox += $"Field:{item.Name} - Error: {item.ToolTip}\n";
                    checker = false;
                }

            }

            if (_Date.ToolTip!=null||_Date.SelectedDate==null)
            {
                checker = false;
                ErrorBox += $"Field:{_Date.Name} - Error: {_Date.ToolTip}\n";
            }

            if (!checker)
            {
                MessageBox.Show(ErrorBox,"Invalid data!");
            }

            return checker;
        }
    }
}
