using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;

namespace SomerenDAL
{
    public class StudentDao : BaseDao
    {      
        public List<Student> GetAllStudents()
        {
            string query = "SELECT studentID, studentFirstName, studentLastName FROM Students";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Student> ReadTables(DataTable dataTable)
        {
            List<Student> students = new List<Student>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Student student = new Student()
                {
                    Id = (int)dr["studentID"],
                    //Female = Convert.ToBoolean(dr["studentFemale"]),
                    FirstName = (string)dr["studentFirstName"],
                    LastName = (string)dr["studentLastName"]
                    //Age = (int)dr["studentAge"],
                    //InternationalGroup = Convert.ToBoolean(dr["studentInternational"]),
                    //RoomNumber = (int)dr["roomNumber"]
                };
                students.Add(student);
            }
            return students;
        }
    }
}
