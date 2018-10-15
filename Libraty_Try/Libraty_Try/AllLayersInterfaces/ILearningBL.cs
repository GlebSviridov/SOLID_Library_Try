using Library_Try.AllLayersInterfaces;
using Library_Try.Models;

namespace Library_Try.BusinessLogicLayer
{
    public interface ILearningBL
    {
        void SetMark(StudentAndLections journalRow, int mark, bool coming, ISender sender);
    }
}