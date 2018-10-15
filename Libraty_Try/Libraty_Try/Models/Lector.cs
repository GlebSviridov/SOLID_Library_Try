using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Try.StructuresOfId;

namespace Library_Try.Models
{
    public class Lector
    {
        public LectorId lectorId;
        public string firstName;
        public string secondName;
        public string email; 

        public Lector()
        {

        }

        public Lector(LectorId lectId, string fName, string sName, string email)
        {
            lectorId = lectId;
            firstName = fName;
            secondName = sName;
            this.email = email;
        }
    }
}
