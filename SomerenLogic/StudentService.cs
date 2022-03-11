using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenLogic
{
    public class StudentService
    {
        //new StudentDao
        StudentDao studentdb;

        public StudentService()
        {
            //Initialize studentdb
            studentdb = new StudentDao();
        }

        public List<Student> GetStudents()
        {
            //use the StudentDao object to return a list of students
            List<Student> students = studentdb.GetAllStudents();
            return students;
        }
    }
}
