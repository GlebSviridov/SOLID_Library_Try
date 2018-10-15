using System;
using System.Collections.Generic;
using System.Linq;
using Library_Try.AllLayersInterfaces;
using Library_Try.ConnectedLayer;
using Library_Try.Helpers;
using Library_Try.Models;

namespace Library_Try.BusinessLogicLayer
{
    public class LearningBL : IAdder, ILearningBL
    {
        private readonly ILogger _logger;
        private readonly IConfig _config;
        private readonly IStudentsDal _studentsDal;
        private readonly IStudentsAndLectionsDal _studentsAndLectionsDal;
        private readonly ILectionsDal _lectionsDal;
        private readonly ILectorsDal _lectorsDal;
        private const int _avgMark = 4;
        private const string _messageToSend = "You have a bad mark";
        private const string _universityEmail = "Epam@test.ru";


        public LearningBL(IConfig config, ILogger logger, IStudentsDal studentsDal, IStudentsAndLectionsDal studentsAndLectionsDal, ILectionsDal lectionsDal, ILectorsDal lectorsDal)
        {
            _logger = logger;
            _studentsDal = studentsDal;
            _studentsAndLectionsDal = studentsAndLectionsDal;
            _lectionsDal = lectionsDal;
            _lectorsDal = lectorsDal;
            _config = config;
        }

        public void AddLection(Lection lection)
        {
            if (lection == null)
            {
                _logger.Log("You sent empty lection in AddLection");
                throw new NullReferenceException("You sent empty lection in AddLection");
            }
            _lectionsDal.InsertLection(lection);
        }

        public void AddLector(Lector lector)
        {
            if (lector == null)
            {
                _logger.Log("You sent empty lector in AddLector");
                throw new NullReferenceException("You sent empty lector in AddLector");
            }
            _lectorsDal.InsertLector(lector);
        }

        public void AddStudent(Student student)
        {
            if (student == null)
            {
                _logger.Log("You sent empty student in AddStudent");

                throw new NullReferenceException("You sent empty student in AddStudent");
            }
            _studentsDal.InsertStudent(student);
        }

        public void AddInJournal(StudentAndLections journalRow)
        {
            if (journalRow == null)
            {
                _logger.Log("You sent empty journalRow in AddInJournal");

                throw new NullReferenceException("You sent empty journalRow in AddInJournal");
            }
            _studentsAndLectionsDal.InsertStudentAndLections(journalRow);
        }




        public void SetMark(StudentAndLections journalRow, int mark, bool coming, ISender sender)
        {
            if (journalRow == null)
            {
                _logger.Log("You sent empty journalRow in SetMark");

                throw new NullReferenceException("You sent empty journalRow in SetMark");
            }
            journalRow.homework = coming;
            journalRow.presence = coming;
            if (journalRow.homework && journalRow.presence)
            {
                if (mark >=1 && mark <= 5)
                {
                    journalRow.mark = mark;
                    UpdateAvgMark(journalRow.student, sender);
                    UpdateVisiting(journalRow.student, journalRow.lection.lector, sender);
                }

            }

            _studentsAndLectionsDal.UpdateStudentAndLections(journalRow.student.studentId, journalRow.lection.lectionId, journalRow);
        }


        private void UpdateAvgMark(Student student, ISender sender)
        {
            var studentAndLectionsList = _studentsAndLectionsDal.GetAllStudentsAndLections();
            var resultList = studentAndLectionsList.Where(s => s.student.studentId.Id == student.studentId.Id).Select(s => s).ToList();
            student.averageMark = CalculateAvgMark(resultList);
            if (resultList.Count != 0)
            {
                if (student.averageMark < _avgMark)
                {
                    sender.Send(_messageToSend, _universityEmail, student.email);
                }
            }
        }


        private int CalculateAvgMark(List<StudentAndLections> salList)
        {
            int sumMark = 0;
            if (salList.Count != 0)
            {
                foreach (var s in salList)
                {
                    sumMark += s.mark;
                }

                return sumMark / salList.Count;
            }
            else return 0;
        }

        private void UpdateVisiting(Student student, Lector lector, ISender sender)
        {
            var studentAndLectionsList = _studentsAndLectionsDal.GetAllStudentsAndLections();
            var resultList = studentAndLectionsList.Where(s => s.student.studentId.Id == student.studentId.Id).ToList();
            var sumPresence = 0;
            foreach (var s in resultList)
            {
                if (s.presence)
                {
                    sumPresence += 1;
                }
            }

            student.visiting = sumPresence;
            if (resultList.Count - student.visiting > 3)
            {
                sender.Send(_messageToSend, _universityEmail, student.email);
                sender.Send(_messageToSend, _universityEmail, lector.email);
            }
        }
    }
}