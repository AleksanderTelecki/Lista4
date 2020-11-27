using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Lista3
{
    /// <summary>
    /// Interaction logic for OutputWindow.xaml
    /// </summary>
    public partial class OutputWindow : Window
    {

        List<TextBox> textBoxes = new List<TextBox>();
        Student student = new Student();
        public OutputWindow()
        {
            InitializeComponent();
            textBoxes.Add(_Name);
            textBoxes.Add(_SurName);
            textBoxes.Add(_City);
            textBoxes.Add(_Age);
            textBoxes.Add(_Pesel);
            textBoxes.Add(_NrAlbumu);
            textBoxes.Add(_Adress);
            Student.Copy(MainWindow.Students[MainWindow.Index], student);
            _DataGrid.DataContext = student;
            _Date.SelectedDateChanged += _Date_SelectedDateChanged;
            
        }

        private void _Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_Date.SelectedDate != null)
            {


                var Text = (DateTime.Today - _Date.SelectedDate).Value.Days.ToString();
                int age = (int.Parse(Text) / 365);
                if (age < 18 || age > 90)
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

        private void _Save_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{



            //    if (CheckText())
            //    {


            //        MainWindow.Students[MainWindow.Index].Name = _Name.Text;
            //        MainWindow.Students[MainWindow.Index].SurName = _SurName.Text;
            //        MainWindow.Students[MainWindow.Index].HomeCity = _City.Text;
            //        MainWindow.Students[MainWindow.Index].Pesel = _Pesel.Text;
            //        MainWindow.Students[MainWindow.Index].Age = int.Parse(_Age.Text);
            //        MainWindow.Students[MainWindow.Index].DateOfBirth = _Date.SelectedDate.Value;
            //        MainWindow.Students[MainWindow.Index].Adress = _Adress.Text;
            //        MainWindow.Students[MainWindow.Index].NumerAlbumu = _NrAlbumu.Text;

            //        MessageBox.Show("Success!");
            //        MainWindow myWindow = Application.Current.MainWindow as MainWindow;
            //        myWindow.Refresh();
            //    }
              


            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Incorect Data");

            //}
        }


        private bool CheckText()
        {

            string ErrorBox = "";
            bool checker = true;
            foreach (var item in textBoxes)
            {
                if (item.ToolTip != null)
                {
                    ErrorBox += $"Field:{item.Name} - Error: {item.ToolTip}\n";
                    checker = false;
                }

            }

            if (_Date.ToolTip != null || _Date.SelectedDate == null)
            {
                checker = false;
                ErrorBox += $"Field:{_Date.Name} - Error: {_Date.ToolTip}\n";
            }

            if (!checker)
            {
                MessageBox.Show(ErrorBox, "Invalid data!");
            }

            return checker;
        }

        private void _Delete_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Students.Remove(MainWindow.Students[MainWindow.Index]);
            MessageBox.Show("Success!");
            this.Close();
        }

        private void _ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog saveFileDialog = new OpenFileDialog();
            //if (saveFileDialog.ShowDialog() == true)
            //{
            //    MainWindow.Students[MainWindow.Index].PersonStringImage = StudentCheker.ImgToStr(saveFileDialog.FileName);
            //}

            //_Image.Source = MainWindow.Students[MainWindow.Index].PersonImage;


        }
    }
}
