using System;
using System.Data;
using System.Data.SqlClient;

namespace Server.Functions
{
    /// <summary>
    /// Provides interactivity with SQL Server database
    /// </summary>
    public class Database
    {
        private static string connectionString
        {
            get
            {
                string conString = string.Format("Server={0};Database={1};", OPT.GetString("db.auth.ip"), OPT.GetString("db.auth.name"));
                if (OPT.GetString("db.auth.trusted") == "1") { conString += "Trusted_Connection=True;"; }
                else { conString += string.Format("uid={0};pwd={1};", OPT.GetString("db.auth.user"), OPT.GetString("db.auth.password")); }

                return conString;
            }
        }

        /// <summary>
        /// Creates a new SqlConnection for future uses
        /// </summary>
        /// <returns>Prepared SqlConnection</returns>
        public static SqlConnection Connection { get { return new SqlConnection(connectionString); } }

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

                // Catch the null object (for executeType 0 & 1
                if (returnObj == null && executeType <= 1)
                {
                    returnObj = 0;
                }

                return returnObj;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("\n\tWARNING SQL Error:\n{0}", sqlEx.Message);
                return null;
            }
        }
    }
}
