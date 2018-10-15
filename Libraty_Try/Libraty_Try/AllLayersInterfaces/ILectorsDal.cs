using System.Collections.Generic;
using Library_Try.Models;
using Library_Try.StructuresOfId;

namespace Library_Try.ConnectedLayer
{
    public interface ILectorsDal
    {
        void DeleteLector(LectorId lectId);
        List<Lector> GetAllLectors();
        Lector GetLectorById(LectorId lectId);
        void InsertLector(Lector lector);
        void UpdateLector(LectorId lectId, Lector lector);
    }
}