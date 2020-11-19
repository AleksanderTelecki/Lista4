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

namespace Lista3
{

    [Serializable]
   public class Student
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
