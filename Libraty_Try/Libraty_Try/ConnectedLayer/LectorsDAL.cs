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
    public class LectorsDal : ILectorsDal
    {
        private string _connectionString;
        private ILogger _logger;
        public LectorsDal(IConfig config, ILogger logger)
        {
            _connectionString = config.ConnectionString;
            _logger = logger;
        }

        public LectorsDal()
        {
        }

        public void InsertLector(Lector lector)
        {
            if (lector == null)
            {
                _logger.Log(string.Format("You sent a null lector:\n {0}", nameof(NullReferenceException)));
            }
            string sql = "Insert Into Lectors " +
                         "(firstName, secondName, email) Values " +
                         "(@firstName, @secondName, @email) ";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@firstName", lector.firstName);
                        command.Parameters.AddWithValue("@secondName", lector.secondName);
                        command.Parameters.AddWithValue("@email", lector.email);
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

        public void UpdateLector(LectorId lectId, Lector lector)
        {
            if (lector == null)
            {
                _logger.Log(string.Format("You sent a null lector:\n {0}", nameof(NullReferenceException)));
            }
            string sql = "Update Lectors Set " +
                         "firstName = @firstName, secondName = @secondName, email = @email " +
                         "Where lectorId = @lectorId";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@lectorId", lectId.Id);
                        command.Parameters.AddWithValue("@firstName", lector.firstName);
                        command.Parameters.AddWithValue("@secondName", lector.secondName);
                        command.Parameters.AddWithValue("@email", lector.email);
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

        public void DeleteLector(LectorId lectId)
        {
            string sql = "Delete from Lectors " +
                         "Where lectorId = @lectorId ";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@lectorId", lectId.Id);
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


        public Lector GetLectorById(LectorId lectId)
        {
            var result = new Lector();

            string sql = "Select LectorId, FirstName, SecondName, Email from Lectors where lectorId=@lectorId";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@lectorId", lectId.Id);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!Int32.TryParse(reader["LectorId"].ToString(), out result.lectorId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            result.firstName = (string) reader["firstName"];
                            result.secondName = (string) reader["secondName"];
                            result.email = (string) reader["email"];
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

        public List<Lector> GetAllLectors()
        {
            List<Lector> lst = new List<Lector>();
            string sql = "Select LectorId, FirstName, SecondName, Email From Lectors";
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
                            var result = new Lector();
                            if (!Int32.TryParse(reader["LectorId"].ToString(), out result.lectorId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            result.firstName = (string)reader["firstName"];
                            result.secondName = (string)reader["secondName"];
                            result.email = (string)reader["email"];
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
