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
            _DataGrid.DataContext = MainWindow.Persons[MainWindow.Index];
            
        }

        private void _Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {



                if (CheckText())
                {


                    MainWindow.Persons[MainWindow.Index].Name = _Name.Text;
                    MainWindow.Persons[MainWindow.Index].SurName = _SurName.Text;
                    MainWindow.Persons[MainWindow.Index].HomeCity = _City.Text;
                    MainWindow.Persons[MainWindow.Index].Pesel = _Pesel.Text;
                    MainWindow.Persons[MainWindow.Index].Age = int.Parse(_Age.Text);
                    MainWindow.Persons[MainWindow.Index].DateOfBirth = _Date.SelectedDate.Value;
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

        }

        private void _ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                MainWindow.Persons[MainWindow.Index].PersonStringImage = Person.ImgToStr(saveFileDialog.FileName);
            }

            _Image.Source = MainWindow.Persons[MainWindow.Index].PersonImage;


        }
    }
}
