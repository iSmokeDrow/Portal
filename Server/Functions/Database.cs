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
                string conString = string.Format("Server={0};Database={1};", OPT.SettingsList["db.auth.ip"], OPT.SettingsList["db.auth.name"]);
                if (OPT.SettingsList["db.auth.trusted"] == "1") { conString += "Trusted_Connection=True;"; }
                else { conString += string.Format("uid={0};pwd={1};", OPT.SettingsList["db.auth.user"], OPT.SettingsList["db.auth.password"]); }

                return conString;
            }
        }

        /// <summary>
        /// Creates a new SqlConnection for future uses
        /// </summary>
        /// <param name="connectionString">Connection settings for this connection</param>
        /// <returns>Prepared SqlConnection</returns>
        public static SqlConnection CreateConnection { get { return new SqlConnection(connectionString); } }

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
                Console.WriteLine("SQL Error:\n{0}", sqlEx.Message);
                return null;
            }
        }
    }
}
