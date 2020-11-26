using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using System.Drawing;
using Lista3.Properties;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Lista3
{

    [Serializable]
   public class Student:IDataErrorInfo
    {

        public string Name { get; set; }
        public string SurName { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string ID { get; set; }
        public string HomeCity { get; set; }
        public string Adress { get; set; }
        public string NumerAlbumu { get; set; }

        [XmlIgnore]
        public string Error
        {
            get
            {
               
                return null;
            }
        }

        [XmlIgnore]
        public Dictionary<string, string> ErrorsColl { get; set; } = new Dictionary<string, string>();

        [XmlIgnore]
        public string this[string name]
        {

            get
            {
                string result = null;


                switch (name)
                {

                    case "Name":
                        result = ValidateName();
                        break;
                    case "NumerAlbumu":
                        result = ValidateNrAlbum();
                        break;
                    case "SurName":
                        result = ValidateSurName();
                        break;
                    case "HomeCity":
                        result = ValidateCityName();
                        break;
                    case "Adress":
                        result = ValidateAdress();
                        break;
                    case "Pesel":
                        result = ValidatePesel();
                        break;
                   




                }

                if (ErrorsColl.ContainsKey(name))
                {
                    ErrorsColl[name] = result;
                }
                else if(result!=null)
                {
                    ErrorsColl.Add(name, result);
                }

               
                
                return result;
            }

        }

        private string ValidateCityName()
        {
            if (String.IsNullOrEmpty(HomeCity))
            {
                return "Cant Null";
            }

            if (Regex.IsMatch(HomeCity, @"\d"))
            {
                return "Cant Numerical";
            }

            return null;
        }

       

        private string ValidatePesel()
        {

            if (String.IsNullOrEmpty(Pesel))
            {
                return "Cant be empty";
            }


          

            if (Pesel.Length < 11)
            {
                return "Must be 11 numbers";
            }

            if (Pesel.Length > 11)
            {
                return "Can't be more then 11 numbers";
            }

            if ((double.TryParse(Pesel,out double redf))!=true)
            {

                return "Must be numerical";

            }



            return null;
        }

        private string ValidateNrAlbum()
        {


            if (String.IsNullOrEmpty(NumerAlbumu))
            {
                return "Cant be empty";
            }


            if (!(int.TryParse(NumerAlbumu, out int res)))
            {

                return "Must be numerical";

            }

            if (NumerAlbumu.Length<6)
            {
                return "Must be 6 numbers";
            }

            if (NumerAlbumu.Length>6)
            {
                return "Can't be more then 6 numbers";
            }

           

          

            return null;

        }

        private string ValidateName()
        {
            if (String.IsNullOrEmpty(Name))
            {
                return "Cant Null";
            }

            if (Regex.IsMatch(Name, @"\d"))
            {
                return "Cant Numerical";
            }

            return null;
            
        }

        private string ValidateSurName()
        {
            if (String.IsNullOrEmpty(SurName))
            {
                return "Cant Null";
            }

            if (Regex.IsMatch(SurName, @"\d"))
            {
                return "Cant Numerical";
            }

            return null;

        }

        private string ValidateAdress()
        {
            if (String.IsNullOrEmpty(Adress))
            {
                return "Cant Null";
            }

            if (Adress.Length>30)
            {
                return "Can't be more then 30 symbols";
            }



            return null;

        }

        public Student(string name, string surname, string pesel,string homecity,DateTime date,int age,string adress,string nralb)
        {
            Name = name;
            SurName = surname;
            Pesel = pesel;
            ID = Guid.NewGuid().ToString();
            HomeCity = homecity;
            DateOfBirth = date;
            Age = age;
            Adress = adress;
            NumerAlbumu = nralb;


        }

        [XmlIgnore]
        public BitmapImage PersonImage
        {


            get
            {
                if (String.IsNullOrEmpty(PersonStringImage))
                {

                    return BitmapToImageSource((Bitmap)Resources.no_image);
                }

                return BitmapToImageSource((Bitmap)StrToImg(PersonStringImage));
            }
           
        }
       
        public string PersonStringImage { get; set; }


    

        public Student()
        {

        }

        public void SetId()
        {
            ID = Guid.NewGuid().ToString();
        }


        public static string ImgToStr(string filename)
        {
            MemoryStream Memostr = new MemoryStream();
            System.Drawing.Image Img = System.Drawing.Image.FromFile(filename);
            Img.Save(Memostr, Img.RawFormat);
            byte[] arrayimg = Memostr.ToArray();
            return Convert.ToBase64String(arrayimg);
        }

        public static System.Drawing.Image StrToImg(string StrImg)
        {
            byte[] arrayimg = Convert.FromBase64String(StrImg);
            System.Drawing.Image imageStr = System.Drawing.Image.FromStream(new MemoryStream(arrayimg));
            return imageStr;
        }


        public static void Copy(Student copyfrom, Student copyto)
        {
            copyto.Adress = copyfrom.Adress;
            copyto.Name = copyfrom.Name;
            copyto.SurName = copyfrom.SurName;
            copyto.Pesel = copyfrom.Pesel;
            copyto.ID = copyfrom.ID;
            copyto.HomeCity = copyfrom.HomeCity;
            copyto.NumerAlbumu = copyfrom.NumerAlbumu;
            copyto.Age = copyfrom.Age;
            copyto.DateOfBirth = copyfrom.DateOfBirth;
            copyto.PersonStringImage = copyfrom.PersonStringImage;
            
  

        }


        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

    }
}
