using System.Collections.Generic;
using Library_Try.Models;
using Library_Try.StructuresOfId;

namespace Library_Try.ConnectedLayer
{
    public interface IStudentsDal
    {
        void DeleteStudent(StudentId studId);
        List<Student> GetAllStudents();
        Student GetStudentById(StudentId studId);
        void InsertStudent(Student student);
        void UpdateStudent(StudentId studId, Student student);
    }
}