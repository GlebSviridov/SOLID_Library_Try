using Library_Try.Models;

namespace Library_Try.AllLayersInterfaces
{
    public interface IAdder
    {
        void AddLection(Lection lection);
        void AddLector(Lector lector);
        void AddStudent(Student student);
        void AddInJournal(StudentAndLections sal);
    }
}