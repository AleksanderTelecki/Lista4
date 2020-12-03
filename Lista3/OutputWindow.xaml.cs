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
            StudentsEntities entities = new StudentsEntities();
            student = entities.Student.Where(item => item.id_stud == MainWindow.IndexID).FirstOrDefault();
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
            try
            {



                if (CheckText())
                {

                    StudentsEntities entities = new StudentsEntities();
                    student = entities.Student.Where(item => item.id_stud == MainWindow.IndexID).FirstOrDefault();





                    student.NAME = _Name.Text;
                    student.SURNAME = _SurName.Text;
                    student.ADRESS_CITY = _City.Text;
                    student.PESEL = _Pesel.Text;
                    student.AGE = int.Parse(_Age.Text);
                    student.BIRTHDATE = _Date.SelectedDate.Value;
                    student.ADRESS_STREET = _Adress.Text;
                    student.NR_ALBUM = _NrAlbumu.Text;
                    entities.SaveChanges();


                    MessageBox.Show("Success!");
                    MainWindow myWindow = Application.Current.MainWindow as MainWindow;
                    myWindow.Refresh();
                }
              


            }
            catch (Exception)
            {
                MessageBox.Show("Incorect Data");

            }
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
            StudentsEntities entities = new StudentsEntities();
            student = entities.Student.Where(item => item.id_stud == MainWindow.IndexID).FirstOrDefault();
            entities.Student.Remove(student);
            entities.SaveChanges();


            MessageBox.Show("Success!");
            this.Close();
        }

        private void _ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            StudentsEntities entities = new StudentsEntities();
            student = entities.Student.Where(item => item.id_stud == MainWindow.IndexID).FirstOrDefault();


            OpenFileDialog saveFileDialog = new OpenFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                student.AddImage(new Bitmap(saveFileDialog.FileName));
                _Image.Source = student.GetImage();
                entities.SaveChanges();
            }


        }
    }
}
