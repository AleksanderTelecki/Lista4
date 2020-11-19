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
            _DataGrid.DataContext = MainWindow.Students[MainWindow.Index];
            
        }

        private void _Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {



                if (CheckText())
                {


                    MainWindow.Students[MainWindow.Index].Name = _Name.Text;
                    MainWindow.Students[MainWindow.Index].SurName = _SurName.Text;
                    MainWindow.Students[MainWindow.Index].HomeCity = _City.Text;
                    MainWindow.Students[MainWindow.Index].Pesel = _Pesel.Text;
                    MainWindow.Students[MainWindow.Index].Age = int.Parse(_Age.Text);
                    MainWindow.Students[MainWindow.Index].DateOfBirth = _Date.SelectedDate.Value;
                    MainWindow.Students[MainWindow.Index].Adress = _Adress.Text;
                    MainWindow.Students[MainWindow.Index].NumerAlbumu = _NrAlbumu.Text;
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
                    checker = false;
                }
                else
                {
                    item.Background = Brushes.White;
                }

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
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                MainWindow.Students[MainWindow.Index].PersonStringImage = Student.ImgToStr(saveFileDialog.FileName);
            }

            _Image.Source = MainWindow.Students[MainWindow.Index].PersonImage;


        }
    }
}
