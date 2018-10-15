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
    public class LectionsDal : ILectionsDal
    {
        private string _connectionString;
        private ILogger _logger;
        public LectionsDal(IConfig config, ILogger logger)
        {
            _connectionString = config.ConnectionString;
            _logger = logger;
        }

        public LectionsDal()
        {

        }

        public void InsertLection(Lection lection)
        {
            if (lection == null)
            {
                _logger.Log(string.Format("You sent a null lection:\n {0}", nameof(NullReferenceException)));
            }
            string sql = "Insert Into lection " +
                         "(lectorId, lectionName, lectionData) Values " +
                         "(@lectorId, @lectionName, @lectionData)";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@lectorId",lection.lector.lectorId.Id );
                        command.Parameters.AddWithValue("@lectionName", lection.lectionName);
                        command.Parameters.AddWithValue("@lectionData", lection.lectionData);
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

        public void UpdateLection(LectionId lectionId, Lection lection)
        {
            if (lection == null)
            {
                _logger.Log(string.Format("You sent a null lection:\n {0}", nameof(NullReferenceException)));
            }
            string sql = "Update lection Set " +
                         "lectorId = @lectorId, lectionName = @lectionName, lectionData = @lectionData " +
                         "Where lectionId = @lectionId";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@lectionId", lectionId.Id);
                        command.Parameters.AddWithValue("@lectorId", lection.lector.lectorId.Id);
                        command.Parameters.AddWithValue("@lectionName", lection.lectionName);
                        command.Parameters.AddWithValue("@lectionData", lection.lectionData);
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

        public void DeleteLection(LectionId lectionId)
        {
            string sql = "Delete from lection " +
                         "Where lectionId = @lectionId";
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@lectorId", lectionId.Id);
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


        


        public Lection GetLecionById(LectionId lectionId)
        {
            var result = new Lection();
            result.lector = new Lector();

            string sql = string.Format("SELECT L.LectionId, L.LectionName, L.LectionData, " +
                                       "LRS.FirstName, LRS.SecondName, LRS.Email, LRS.LectorId " +
                                       "From Lection, Lectors " +
                                       "INNER JOIN Lection L ON L.LectionId = L.LectionId " +
                                       "INNER JOIN Lectors LRS ON L.LectorId  = LRS.LectorId " +
                                       "Where L.LectionId = @LectionId ");
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@LectionId", lectionId.Id);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!Int32.TryParse(reader["LectionId"].ToString(), out result.lectionId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            if (!Int32.TryParse(reader["LectorId"].ToString(), out result.lector.lectorId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }

                            result.lector.firstName = (string) reader["FirstName"];
                            result.lector.secondName = (string) reader["SecondName"];
                            result.lector.email = (string) reader["Email"];
                            result.lectionName = (string) reader["lectionName"];
                            result.lectionData = (DateTime) reader["lectionData"];
                        }

                    }
                }
                return result;
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

        public List<Lection> GetAllLections()
        {
            List<Lection> lst = new List<Lection>();
            string sql = "SELECT L.LectionId, L.LectionName, L.LectionData, " +
                         "LRS.FirstName, LRS.SecondName, LRS.Email, LRS.LectorId " +
                         "From Lection, Lectors " +
                         "INNER JOIN Lection L ON L.LectionId = L.LectionId " +
                         "INNER JOIN Lectors LRS ON L.LectorId  = LRS.LectorId ";
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
                            var result = new Lection();
                            result.lector = new Lector();
                            if (!Int32.TryParse(reader["LectionId"].ToString(), out result.lectionId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }
                            if (!Int32.TryParse(reader["LectorId"].ToString(), out result.lector.lectorId.Id))
                            {
                                //TODO  Add exception Name and Logging
                                throw new FormatException();
                            }

                            result.lector.firstName = (string)reader["FirstName"];
                            result.lector.secondName = (string)reader["SecondName"];
                            result.lector.email = (string)reader["Email"];
                            result.lectionName = (string)reader["lectionName"];
                            result.lectionData = (DateTime)reader["lectionData"];
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
