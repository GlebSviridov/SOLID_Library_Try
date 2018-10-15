using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Try.Models
{
    public class StudentAndLections
    {
        public StudentAndLections(Lection lection, int mark, Student student, bool presence, bool homework)
        {
            this.lection = lection;
            this.mark = mark;
            this.student = student;
            this.presence = presence;
            this.homework = homework;
        }

        public StudentAndLections(Lection lection, Student student)
        {
            this.lection = lection;
            mark = 0;
            this.student = student;
            presence = false;
            homework = false;
        }

        public StudentAndLections()
        {
        }

        public Student student;
        public Lection lection;
        public int mark;
        public bool presence;
        public bool homework;

    }
}
