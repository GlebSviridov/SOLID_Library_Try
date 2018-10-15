using System.Collections.Generic;
using Library_Try.Models;
using Library_Try.StructuresOfId;

namespace Library_Try.ConnectedLayer
{
    public interface IStudentsAndLectionsDal
    {
        void DeleteStudentAndLections(StudentId studId, LectionId lectionId);
        List<StudentAndLections> GetAllStudentsAndLections();
        StudentAndLections GetStudentAndLectionsById(StudentId studId, LectionId lectionId);
        void InsertStudentAndLections(StudentAndLections sal);
        void UpdateStudentAndLections(StudentId studId, LectionId lectionId, StudentAndLections sal);
    }
}