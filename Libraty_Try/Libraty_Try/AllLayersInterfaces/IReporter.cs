using Library_Try.StructuresOfId;

namespace Library_Try.AllLayersInterfaces
{
    public interface IReporter
    {
        void Report(StudentId studentId, string pathToReport);
    }
}