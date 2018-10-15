using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Library_Try.AllLayersInterfaces;
using Library_Try.ConnectedLayer;
using Library_Try.Helpers;
using Library_Try.StructuresOfId;
using Library_Try.Models;

namespace Library_Try.Reporters
{
    public class ReportXlm : IReporter
    {
        private readonly ILogger _logger;
        private readonly IConfig _config;
        private readonly IStudentsDal _studentsDal;
        private readonly IStudentsAndLectionsDal _studentsAndLectionsDal;
        private readonly ILectionsDal _lectionsDal;
        private readonly ILectorsDal _lectorsDal;

        public ReportXlm(IConfig config, ILogger logger, IStudentsDal studentsDal, IStudentsAndLectionsDal studentsAndLectionsDal, ILectionsDal lectionsDal, ILectorsDal lectorsDal)
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
            using (XmlTextWriter xmlWriter = new XmlTextWriter(pathToReport, Encoding.UTF8))
            {
                xmlWriter.WriteStartElement("List of presence");
                foreach (var l in studentIdList)
                {
                    xmlWriter.WriteElementString("StudentId", l.student.studentId.Id.ToString());
                    xmlWriter.WriteElementString("LectionName", l.lection.lectionName);
                    xmlWriter.WriteElementString("First Name", l.student.firstName);
                    xmlWriter.WriteElementString("Second Name", l.student.secondName);
                    xmlWriter.WriteElementString("LectionDate", l.lection.lectionData.ToString());
                    xmlWriter.WriteElementString("Presence", l.presence.ToString());
                    
                    xmlWriter.Flush();
                }
                xmlWriter.WriteEndElement();
                

            }
        }
    }
}