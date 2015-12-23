using System;
using System.Data;
using System.Data.SqlClient;

namespace Portal.Functions
{
    /// <summary>
    /// Provides interactivity with SQL Server database
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Creates a new SqlConnection for future uses
        /// </summary>
        /// <param name="connectionString">Connection settings for this connection</param>
        /// <returns>Prepared SqlConnection</returns>
        public static SqlConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Executes a prepared Sql Statement on a prepared and opened Sql Connection
        /// </summary>
        /// <param name="inSqlCommand">Prepared SQL Command statement</param>
        /// <param name="executeType">Type of execution</param>
        /// <returns>Filled object</returns>
        public static object ExecuteStatement(SqlCommand inSqlCommand, int executeType)
        {
            try
            {
                inSqlCommand.Connection.Open();

                object returnObj = null;

                switch (executeType)
                {
                    case 0: // Return rows affected
                        returnObj = inSqlCommand.ExecuteNonQuery();
                        break;

                    case 1: // Return one column
                        returnObj = inSqlCommand.ExecuteScalar();
                        break;

                    case 2: //Return filled datatable
                        DataTable tempTable = new DataTable();
                        using (SqlDataReader tempReader = inSqlCommand.ExecuteReader())
                        {
                            tempTable.Load(tempReader);
                            returnObj = tempTable;
                        }
                        break;
                }

                inSqlCommand.Connection.Close();
                return returnObj;
            }
            catch (SqlException sqlEx)
            {
                // say error
                return null;
            }
            catch (Exception ex)
            {
                // say error
                return null;
            }
        }
    }
}
