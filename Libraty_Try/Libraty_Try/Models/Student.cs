using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Try.StructuresOfId;

namespace Library_Try.Models
{
    public class Student
    {
        public StudentId studentId;
        public string firstName;
        public string secondName;
        public int averageMark;
        public int visiting;
        public string phoneNumber;
        public string email;

        public Student()
        {
        }

        public Student(StudentId studId, string fName, string sName, int avgMark, int visiting, string phoneNumber, string email)
        {
            studentId = studId;
            firstName = fName;
            secondName = sName;
            averageMark = avgMark;
            this.visiting = visiting;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }
    }
}
