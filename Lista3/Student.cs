using Lista3.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;


namespace Lista3
{


    public partial class Student : IDataErrorInfo
    {
        public string id_stud { get; set; }
        public string SURNAME { get; set; }
        public string NAME { get; set; }
        public System.DateTime BIRTHDATE { get; set; }
        public int AGE { get; set; }
        public string ADRESS_CITY { get; set; }
        public string ADRESS_STREET { get; set; }
        public string PESEL { get; set; }
        public string NR_ALBUM { get; set; }

        public byte[] STUDENT_IMAGE { get; set; }




        public BitmapImage ImgSource
        {
            get
            {
                return BitmapToImageSource((Bitmap)((new ImageConverter()).ConvertFrom(STUDENT_IMAGE)));
            }
        }



        public string Error
        {
            get
            {

                return null;
            }
        }


        public Dictionary<string, string> ErrorsColl { get; set; } = new Dictionary<string, string>();


        public string this[string name]
        {

            get
            {
                string result = null;


                switch (name)
                {

                    case "NAME":
                        result = ValidateName();
                        break;
                    case "NR_ALBUM":
                        result = ValidateNrAlbum();
                        break;
                    case "SURNAME":
                        result = ValidateSurName();
                        break;
                    case "ADRESS_CITY":
                        result = ValidateCityName();
                        break;
                    case "ADRESS_STREET":
                        result = ValidateAdress();
                        break;
                    case "PESEL":
                        result = ValidatePesel();
                        break;





                }

                if (ErrorsColl.ContainsKey(name))
                {
                    ErrorsColl[name] = result;
                }
                else if (result != null)
                {
                    ErrorsColl.Add(name, result);
                }



                return result;
            }

        }

        private string ValidateCityName()
        {
            if (String.IsNullOrEmpty(ADRESS_CITY))
            {
                return "Pole nie może być puste";
            }

            if (Regex.IsMatch(ADRESS_CITY, @"\d"))
            {
                return "Nie może zawierać symbole numeryczne";
            }

            return null;
        }



        private string ValidatePesel()
        {

            if (String.IsNullOrEmpty(PESEL))
            {
                return "Pole nie może być puste";
            }


            if ((double.TryParse(PESEL, out double redf)) != true)
            {

                return "Pole musi zawierać wyłacznie cyfry";

            }


            if (PESEL.Length < 11)
            {
                return "Musi zawierać 11 cyfr";
            }

            if (PESEL.Length > 11)
            {
                return "Musi zawierać 11 cyfr";
            }





            return null;
        }

        private string ValidateNrAlbum()
        {


            if (String.IsNullOrEmpty(NR_ALBUM))
            {
                return "Pole nie może być puste";
            }


            if (!(int.TryParse(NR_ALBUM, out int res)))
            {

                return "Pole musi zawierać wyłacznie cyfry";

            }

            if (NR_ALBUM.Length < 6)
            {
                return "Musi zawierać 6 cyfr";
            }

            if (NR_ALBUM.Length > 6)
            {
                return "Musi zawierać 6 cyfr";
            }





            return null;

        }

        private string ValidateName()
        {



            if (String.IsNullOrEmpty(NAME))
            {
                return "Pole nie może być puste";
            }


            if (!Regex.IsMatch(NAME, "^[a-zA-Z]+$"))
            {

                return "Pole nie może zawierać inne symbole oprócz liter";
            }

            return null;

        }

        private string ValidateSurName()
        {
            if (String.IsNullOrEmpty(SURNAME))
            {
                return "Pole nie może być puste";
            }

            if (!Regex.IsMatch(SURNAME, "^[a-zA-Z]+$"))
            {

                return "Pole nie może zawierać inne symbole oprócz liter";
            }

            return null;

        }

        private string ValidateAdress()
        {
            if (String.IsNullOrEmpty(ADRESS_STREET))
            {
                return "Pole nie może być puste";
            }

            if (ADRESS_STREET.Length > 30)
            {
                return "Pole nie może zawierać więcej niż 30 symboli";
            }



            return null;

        }

















        public void AddImage(Image img)
        {
            ImageConverter converter = new ImageConverter();
            if (img != null)
            {
                STUDENT_IMAGE = (byte[])converter.ConvertTo(img, typeof(byte[]));
            }
            else
            {
                STUDENT_IMAGE = (byte[])converter.ConvertTo((Bitmap)Resources.no_image, typeof(byte[]));
            }


        }




        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }


        public BitmapImage GetImage()
        {
            return BitmapToImageSource((Bitmap)((new ImageConverter()).ConvertFrom(STUDENT_IMAGE)));
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
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

        public void SetId()
        {
            id_stud = Guid.NewGuid().ToString();
        }

        public static void Copy(Student copyfrom, Student copyto)
        {
            copyto.ADRESS_STREET = copyfrom.ADRESS_STREET;
            copyto.NAME = copyfrom.NAME;
            copyto.SURNAME = copyfrom.SURNAME;
            copyto.PESEL = copyfrom.PESEL;
            copyto.id_stud = copyfrom.id_stud;
            copyto.ADRESS_CITY = copyfrom.ADRESS_CITY;
            copyto.NR_ALBUM = copyfrom.NR_ALBUM;
            copyto.AGE = copyfrom.AGE;
            copyto.BIRTHDATE = copyfrom.BIRTHDATE;
            copyto.STUDENT_IMAGE = copyfrom.STUDENT_IMAGE;



        }


    }
}

