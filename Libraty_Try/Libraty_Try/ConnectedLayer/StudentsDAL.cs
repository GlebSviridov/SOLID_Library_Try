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
using Library_Try.BusinessLogicLayer;

namespace Library_Try.ConnectedLayer
{
    public class StudentsDal : IStudentsDal
    {
        private string _connectionString;
        private ILogger _logger;
        public StudentsDal(IConfig config, ILogger logger)
        {
            _connectionString = config.ConnectionString;
            _logger = logger;
        }

        public StudentsDal()
        {
        }

        public void InsertStudent(Student student)
        {
            if (student == null)
            {
                _logger.Log(string.Format("You sent a null student:\n {0}", nameof(NullReferenceException)));
            }
                
            string sql = "Insert Into Students " +
                         "(FirstName, SecondName, PhoneNumber, Email) Values " +
                         "(@firstName, @secondName, @phoneNumber, @email)";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@firstName", student.firstName);
                        command.Parameters.AddWithValue("@secondName", student.secondName);
                        command.Parameters.AddWithValue("@phoneNumber", student.phoneNumber);
                        command.Parameters.AddWithValue("@email", student.email);
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

        public void UpdateStudent(StudentId studId, Student student)
        {
            if (student == null)
            {
                _logger.Log(string.Format("You sent a null student:\n {0}", nameof(NullReferenceException)));
            }
            string sql = "Update Students Set " +
                         "firstName = @firstName, secondName = @secondName, phoneNumber = @phoneNumber, email = @email " +
                         "Where studentId = @studentId";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@studentId", studId.Id);
                        command.Parameters.AddWithValue("@firstName", student.firstName);
                        command.Parameters.AddWithValue("@secondName", student.secondName);
                        command.Parameters.AddWithValue("@phoneNumber", student.phoneNumber);
                        command.Parameters.AddWithValue("@email", student.email);
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

        public void DeleteStudent(StudentId studId)
        {
            string sql = "Delete from Students " +
                         "Where studentId = @studentId";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@studentId", studId.Id);
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


        public Student GetStudentById(StudentId studId)
        {
            var student = new Student();

            string sql = string.Format("Select StudentId, Firstname, SecondName, PhoneNumber, Email from Students where studentId=@studentId");
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@studentId", studId.Id);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!Int32.TryParse(reader["StudentId"].ToString(), out student.studentId.Id))
                            {
                                throw new FormatException();
                            }

                            student.firstName = reader["FirstName"].ToString();
                            student.secondName = reader["SecondName"].ToString();
                            student.phoneNumber = reader["PhoneNumber"].ToString();
                            student.email = reader["Email"].ToString();
                            student.averageMark = 0;
                            student.visiting = 0;

                        }
                    }
                }

                return student;
            }
            catch (SqlException sqlException)
            {
                _logger.Log(string.Format("You have an error with sql:\n {0}", sqlException));
                return null;
            }
            catch (ArgumentNullException argException)
            {
                _logger.Log(string.Format("You have a null argument:\n {0}", argException));
                return null;
            }



        }

        public List<Student> GetAllStudents()
        {
            List<Student> lst = new List<Student>();
            string sql = "Select StudentId, Firstname, SecondName, PhoneNumber, Email From Students";
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
                            var student = new Student();
                            if (!Int32.TryParse(reader["StudentId"].ToString(), out student.studentId.Id))
                            {
                                throw new FormatException();
                            }

                            student.firstName = reader["FirstName"].ToString();
                            student.secondName = reader["SecondName"].ToString();
                            student.phoneNumber = reader["PhoneNumber"].ToString();
                            student.email = reader["Email"].ToString();
                            student.averageMark = 0;
                            student.visiting = 0;
                            lst.Add(student);
                        }
                    }
                }
            }
            catch (SqlException sqlException)
            {
                _logger.Log(string.Format("You have an error with sql:\n {0}", sqlException));
                return null;
            }
            catch (ArgumentNullException argException)
            {
                _logger.Log(string.Format("You have a null argument:\n {0}", argException));
                return null;
            }

            return lst;
        }
    }
}
