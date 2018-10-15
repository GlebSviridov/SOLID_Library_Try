using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Library_Try.AllLayersInterfaces;
using Library_Try.ConnectedLayer;
using Library_Try.Helpers;
using Library_Try.StructuresOfId;

namespace Library_Try.Reporters
{
    public class ReportTxt : IReporter
    {
        private readonly ILogger _logger;
        private readonly IConfig _config;
        private readonly IStudentsDal _studentsDal;
        private readonly IStudentsAndLectionsDal _studentsAndLectionsDal;
        private readonly ILectionsDal _lectionsDal;
        private readonly ILectorsDal _lectorsDal;

        public ReportTxt(IConfig config, ILogger logger, IStudentsDal studentsDal, IStudentsAndLectionsDal studentsAndLectionsDal, ILectionsDal lectionsDal, ILectorsDal lectorsDal)
        {
            _logger = logger;
            _studentsDal = studentsDal;
            _studentsAndLectionsDal = studentsAndLectionsDal;
            _lectionsDal = lectionsDal;
            _lectorsDal = lectorsDal;
            _config = config;
        }

        public void Report(StudentId studentId, string pathToReport)
        {
            if (String.IsNullOrEmpty(pathToReport))
            {
                _logger.Log("You put incorrect path");
            }
            var studentInformationList = _studentsAndLectionsDal.GetAllStudentsAndLections();
            var studentIdList = studentInformationList.Where(s => s.student.studentId.Id == studentId.Id).ToList();
            using (StreamWriter sWriter = new StreamWriter(pathToReport))
            {
                sWriter.Write("List of presence\n");
                foreach (var l in studentIdList)
                {
                    sWriter.WriteLine(
                        "StudentId : {0} | Lection Name {1} | First Name : {2} | Second Name : {3} | Lection Date : {4} | Presence : {5}",
                        l.student.studentId.Id.ToString(), l.lection.lectionName, l.student.firstName,
                        l.student.secondName, l.lection.lectionData.ToString(), l.presence.ToString());
                }


            }
        }
    }
}