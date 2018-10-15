using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Try.StructuresOfId;

namespace Library_Try.Models
{
    public class Lection
    {
        public LectionId lectionId;
        public Lector lector;
        public string lectionName;
        public DateTime lectionData;

        public Lection()
        {
        }

        public Lection(LectionId lectionId, Lector lector, string name, DateTime dateTime)
        {
            this.lectionId = lectionId;
            this.lector = lector;
            lectionName = name;
            lectionData = dateTime;
        }

    }
}
