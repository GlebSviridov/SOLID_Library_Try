
using System;
using Library_Try.BusinessLogicLayer;
using Library_Try.ConnectedLayer;
using Library_Try.Models;
using Library_Try.StructuresOfId;
using Library_Try.AllLayersInterfaces;
using Library_Try.StructuresOfId;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Library_Try.Tests
{
    [TestClass]
    public class TestBL
    {
       


        [TestMethod]
        public void AddStudent_Student_MethodWasCalled()
        {
            var student = new Student(new StudentId(1), "test", "student", 0, 0, "222222", "stud@mail.ru");
            var mockConfig = new MockConfig("");
            var mockLogger = new MockLogger();
            var studentsDalStab = new Mock<IStudentsDal>();
            var lectorsDalStab = new Mock<ILectorsDal>();
            var lectionsDalStab = new Mock<ILectionsDal>();
            var journalDalStab = new Mock<IStudentsAndLectionsDal>();
            var logic = new LearningBL(mockConfig, mockLogger, studentsDalStab.Object, journalDalStab.Object, lectionsDalStab.Object, lectorsDalStab.Object);
            studentsDalStab.Setup(s =>s.InsertStudent(student)).Verifiable();
            logic.AddStudent(student);
            studentsDalStab.Verify(c => c.InsertStudent(student), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddStudent_Null_NullReferenceException()
        {
            var studentsDalStab = new Mock<IStudentsDal>();
            var lectorsDalStab = new Mock<ILectorsDal>();
            var lectionsDalStab = new Mock<ILectionsDal>();
            var journalDalStab = new Mock<IStudentsAndLectionsDal>();
            var logic = new LearningBL(new MockConfig(""), new MockLogger(), studentsDalStab.Object, journalDalStab.Object, lectionsDalStab.Object, lectorsDalStab.Object);
            logic.AddStudent(null);
        }

        [TestMethod]
        public void AddLection_Lection_MethodWasCalled()
        {
            var lection = new Lection();
            var mockConfig = new MockConfig("");
            var mockLogger = new MockLogger();
            var studentsDalStab = new Mock<IStudentsDal>();
            var lectorsDalStab = new Mock<ILectorsDal>();
            var lectionsDalStab = new Mock<ILectionsDal>();
            var journalDalStab = new Mock<IStudentsAndLectionsDal>();
            var logic = new LearningBL(mockConfig, mockLogger, studentsDalStab.Object, journalDalStab.Object, lectionsDalStab.Object, lectorsDalStab.Object);
            lectionsDalStab.Setup(s => s.InsertLection(lection)).Verifiable();
            logic.AddLection(lection);
            lectionsDalStab.Verify(c => c.InsertLection(lection), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddLection_Null_NullReferenceException()
        {
            var studentsDalStab = new Mock<IStudentsDal>();
            var lectorsDalStab = new Mock<ILectorsDal>();
            var lectionsDalStab = new Mock<ILectionsDal>();
            var journalDalStab = new Mock<IStudentsAndLectionsDal>();
            var logic = new LearningBL(new MockConfig(""), new MockLogger(), studentsDalStab.Object, journalDalStab.Object, lectionsDalStab.Object, lectorsDalStab.Object);
            logic.AddLection(null);
        }

        [TestMethod]
        public void AddLector_Lector_MethodWasCalled()
        {
            var lector = new Lector();
            var mockConfig = new MockConfig("");
            var mockLogger = new MockLogger();
            var studentsDalStab = new Mock<IStudentsDal>();
            var lectorsDalStab = new Mock<ILectorsDal>();
            var lectionsDalStab = new Mock<ILectionsDal>();
            var journalDalStab = new Mock<IStudentsAndLectionsDal>();
            var logic = new LearningBL(mockConfig, mockLogger, studentsDalStab.Object, journalDalStab.Object, lectionsDalStab.Object, lectorsDalStab.Object);
            lectorsDalStab.Setup(s => s.InsertLector(lector)).Verifiable();
            logic.AddLector(lector);
            lectorsDalStab.Verify(c => c.InsertLector(lector), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddLector_Null_NullReferenceException()
        {
            var studentsDalStab = new Mock<IStudentsDal>();
            var lectorsDalStab = new Mock<ILectorsDal>();
            var lectionsDalStab = new Mock<ILectionsDal>();
            var journalDalStab = new Mock<IStudentsAndLectionsDal>();
            var logic = new LearningBL(new MockConfig(""), new MockLogger(), studentsDalStab.Object, journalDalStab.Object, lectionsDalStab.Object, lectorsDalStab.Object);
            logic.AddLector(null);
        }

        [TestMethod]
        public void AddInJournal_JournalRow_MethodWasCalled()
        {
            var journalRow = new StudentAndLections();
            var mockConfig = new MockConfig("");
            var mockLogger = new MockLogger();
            var studentsDalStab = new Mock<IStudentsDal>();
            var lectorsDalStab = new Mock<ILectorsDal>();
            var lectionsDalStab = new Mock<ILectionsDal>();
            var journalDalStab = new Mock<IStudentsAndLectionsDal>();
            var logic = new LearningBL(mockConfig, mockLogger, studentsDalStab.Object, journalDalStab.Object, lectionsDalStab.Object, lectorsDalStab.Object);
            journalDalStab.Setup(s => s.InsertStudentAndLections(journalRow)).Verifiable();
            logic.AddInJournal(journalRow);
            journalDalStab.Verify(c => c.InsertStudentAndLections(journalRow), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddInJournal_Null_NullReferenceException()
        {
            var studentsDalStab = new Mock<IStudentsDal>();
            var lectorsDalStab = new Mock<ILectorsDal>();
            var lectionsDalStab = new Mock<ILectionsDal>();
            var journalDalStab = new Mock<IStudentsAndLectionsDal>();
            var logic = new LearningBL(new MockConfig(""), new MockLogger(), studentsDalStab.Object, journalDalStab.Object, lectionsDalStab.Object, lectorsDalStab.Object);
            logic.AddInJournal(null);
        }
    }
}