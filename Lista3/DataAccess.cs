using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Lista3
{
    public class DataAccess
    {

        public List<Student> GetStudents()
        {

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StudentsDB")))
            {
                return connection.Query<Student>("SELECT * FROM Student").ToList();
            }



        }


        public void AddStudent(string SURNAME, string NAME, DateTime BIRTHDATE, int AGE, string ADRESS_CITY, string ADRESS_STREET, string PESEL, string NR_ALBUM, byte[] STUDENT_IMAGE)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StudentsDB")))
            {

                List<Student> students = new List<Student>();
                Student student = new Student();
                student.NAME = NAME;
                student.SURNAME = SURNAME;
                student.SetId();
                student.BIRTHDATE = BIRTHDATE;
                student.AGE = AGE;
                student.ADRESS_CITY = ADRESS_CITY;
                student.ADRESS_STREET = ADRESS_STREET;
                student.PESEL = PESEL;
                student.NR_ALBUM = NR_ALBUM;
                student.STUDENT_IMAGE = STUDENT_IMAGE;
                students.Add(student);

                connection.Execute("dbo.InsertStudent @id_stud,@SURNAME,@NAME,@BIRTHDATE,@AGE,@ADRESS_CITY,@ADRESS_STREET,@PESEL,@NR_ALBUM,@STUDENT_IMAGE", student);



            }


        }



    }
}
