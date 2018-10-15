using System.Collections.Generic;
using Library_Try.Models;
using Library_Try.StructuresOfId;

namespace Library_Try.ConnectedLayer
{
    public interface ILectionsDal
    {
        void DeleteLection(LectionId lectionId);
        List<Lection> GetAllLections();
        Lection GetLecionById(LectionId lectionId);
        void InsertLection(Lection lection);
        void UpdateLection(LectionId lectionId, Lection lection);
    }
}