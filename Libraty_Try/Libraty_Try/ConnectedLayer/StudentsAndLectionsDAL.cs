using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Try.Helpers;
using Library_Try.Models;
using Library_Try.StructuresOfId;

namespace Library_Try.ConnectedLayer
{
    public class StudentsAndLectionsDal : IStudentsAndLectionsDal
    {
        private string _connectionString;
        private ILogger _logger;
        public StudentsAndLectionsDal(IConfig config, ILogger logger)
        {
            _connectionString = config.ConnectionString;
            _logger = logger;
        }


        public void InsertStudentAndLections(StudentAndLections sal)
        {
            if (sal == null)
            {
                _logger.Log(string.Format("You sent a null journal:\n {0}", nameof(NullReferenceException)));
            }
            string sql = "Insert Into StudentAndLections " +
                         "(studentId, lectionId, mark, presence, homework) Values " +
                         "(@studentId, @lectionId, @mark, @presence, @homework)";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@studentId", sal.student.studentId.Id);
                        command.Parameters.AddWithValue("@lectionId", sal.lection.lectionId.Id);
                        command.Parameters.AddWithValue("@mark", sal.mark);
                        command.Parameters.AddWithValue("@presence", sal.presence);
                        command.Parameters.AddWithValue("@homework", sal.homework);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlException)
            {
                _logger.Log(string.Format("You have an error with sql:\n {0}", sqlException));
            }
            catch (ArgumentNullException argException)
            {
                _logger.Log(string.Format("You have a null argument:\n {0}", argException));
            }
        }

        public void UpdateStudentAndLections(StudentId studId, LectionId lectionId, StudentAndLections sal)
        {
            if (sal == null)
            {
                _logger.Log(string.Format("You sent a null journal:\n {0}", nameof(NullReferenceException)));
            }
            string sql = "Update StudentAndLections Set " +
                         "lectionId = @lectionId, mark = @mark, presence = @presence, homework = @homework " +
                         "Where studentId = @studentId and lectionId = @lectionId";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@studentId", studId.Id);
                        command.Parameters.AddWithValue("@lectionId", lectionId.Id);
                        command.Parameters.AddWithValue("@mark", sal.mark);
                        command.Parameters.AddWithValue("@presence", sal.presence);
                        command.Parameters.AddWithValue("@homework", sal.homework);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlException)
            {
                _logger.Log(string.Format("You have an error with sql:\n {0}", sqlException));
            }
            catch (ArgumentNullException argException)
            {
                _logger.Log(string.Format("You have a null argument:\n {0}", argException));
            }

        }

        public void DeleteStudentAndLections(StudentId studId, LectionId lectionId)
        {
            string sql = "Delete from StudentAndLections " +
                         "Where studentId = @studentId and lectionId = @lectionId ";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@studentId", studId.Id);
                        command.Parameters.AddWithValue("@lectionId", lectionId.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlException)
            {
                _logger.Log(string.Format("You have an error with sql:\n {0}", sqlException));
            }
            catch (ArgumentNullException argException)
            {
                _logger.Log(string.Format("You have a null argument:\n {0}", argException));
            }

        }


        public StudentAndLections GetStudentAndLectionsById(StudentId studId, LectionId lectionId)
        {
            var result = new StudentAndLections();
            result.student = new Student();
            result.lection = new Lection();
            result.lection.lector = new Lector();

            string sql = "SELECT sal.studentId,sal.lectionId,sal.mark,sal.presence,sal.homework, " +
                         "S.firstName, S.secondName, S.email, S.phoneNumber, " +
                         " L.lectionName,L.lectionData, " +
                         "LRS.firstName, LRS.secondName, LRS.lectorId, LRS.email " +
                         "From StudentAndLections sal " +
                         "JOIN Lection L ON sal.lectionId = L.lectionId " +
                         "LEFT JOIN Students S ON sal.studentId = S.studentId  " +
                         "JOIN Lectors LRS ON L.lectorId  = LRS.lectorId " +
                         "Where sal.studentId = @studentId and sal.lectionId = @lectionId ";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@studentId", studId.Id);
                        command.Parameters.AddWithValue("@lectionId", lectionId.Id);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!Int32.TryParse(reader[0].ToString(), out result.student.studentId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }

                            if (!Int32.TryParse(reader[1].ToString(), out result.lection.lectionId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            if (!Int32.TryParse(reader[2].ToString(), out result.mark))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            result.presence = (bool) reader[3];
                            result.homework = (bool) reader[4];
                            result.student.firstName = (string) reader[5];
                            result.student.secondName = (string) reader[6];
                            result.student.email = (string) reader[7];
                            result.student.phoneNumber = (string) reader[8];
                            result.lection.lectionName = (string) reader[9];
                            result.lection.lectionData = (DateTime) reader[10];
                            result.lection.lector.firstName = (string) reader[11];
                            result.lection.lector.secondName = (string) reader[12];
                            if (!Int32.TryParse(reader[13].ToString(), out result.lection.lector.lectorId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            result.lection.lector.email = (string) reader[14];

                        }
                    }
                }
            }
            catch (SqlException sqlException)
            {
                _logger.Log(string.Format("You have an error with sql:\n {0}", sqlException));
            }
            catch (ArgumentNullException argException)
            {
                _logger.Log(string.Format("You have a null argument:\n {0}", argException));
            }

            return result;
        }

        public List<StudentAndLections> GetAllStudentsAndLections()
        {
            List<StudentAndLections> lst = new List<StudentAndLections>();
            string sql = "SELECT sal.studentId,sal.lectionId,sal.mark,sal.presence,sal.homework, " +
                         "S.firstName, S.secondName, S.email, S.phoneNumber, " +
                         " L.lectionName,L.lectionData, " +
                         "LRS.firstName, LRS.secondName, LRS.lectorId, LRS.Email " +
                         "From StudentAndLections sal " +
                         "JOIN Lection L ON sal.lectionId = L.lectionId " +
                         "LEFT JOIN Students S ON sal.studentId = S.studentId  " +
                         "JOIN Lectors LRS ON L.lectorId  = LRS.lectorId ";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var result = new StudentAndLections();
                            result.student = new Student();
                            result.lection = new Lection();
                            result.lection.lector = new Lector();
                            if (!Int32.TryParse(reader[0].ToString(), out result.student.studentId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }

                            if (!Int32.TryParse(reader[1].ToString(), out result.lection.lectionId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            if (!Int32.TryParse(reader[2].ToString(), out result.mark))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            result.presence = (bool)reader[3];
                            result.homework = (bool)reader[4];
                            result.student.firstName = (string)reader[5];
                            result.student.secondName = (string)reader[6];
                            result.student.email = (string)reader[7];
                            result.student.phoneNumber = (string)reader[8];
                            result.lection.lectionName = (string)reader[9];
                            result.lection.lectionData = (DateTime)reader[10];
                            result.lection.lector.firstName = (string)reader[11];
                            result.lection.lector.secondName = (string)reader[12];
                            if (!Int32.TryParse(reader[13].ToString(), out result.lection.lector.lectorId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            result.lection.lector.email = (string)reader[14];
                            lst.Add(result);
                        }
                    }
                }
            }
            catch (SqlException sqlException)
            {
                _logger.Log(string.Format("You have an error with sql:\n {0}", sqlException));
            }
            catch (ArgumentNullException argException)
            {
                _logger.Log(string.Format("You have a null argument:\n {0}", argException));
            }

            return lst;
        }
    }
}
