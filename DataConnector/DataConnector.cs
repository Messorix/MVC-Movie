using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataConnectorNS
{
    public class DataConnector
    {
        #region Singleton
        private DataConnector() { }

        public static DataConnector Instance { get; } = new DataConnector();
        #endregion

        private static SqlCommand Command { get; set; }
        private static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["MvcMovie"].ConnectionString);
            }
        }
        private static string Query { get; set; }

        private static void OpenConnection()
        {
            if (Command.Connection.State != ConnectionState.Open)
                Command.Connection.Open();
        }

        private static int GetRowCount(string tableName)
        {
            string sql = "SELECT COUNT(*) FROM " + tableName;

            Command = new SqlCommand(sql, Connection);
            OpenConnection();
            int rowCount = (int)Command.ExecuteScalar();

            Connection.Close();
            return rowCount;
        }
        
        public static string[,] GetData(string tableName)
        {
            int rowCount = GetRowCount(tableName);

            Query = "SELECT * " +
                    "FROM " + tableName;

            Command = new SqlCommand(Query, Connection);
            OpenConnection();

            SqlDataReader reader = Command.ExecuteReader();
            string[,] returnable = new string[rowCount, reader.FieldCount];
            int row = 0;

            while (reader.Read())
            {
                for (int column = 0; column < reader.FieldCount; column++)
                {
                    returnable[row, column] = reader[column].ToString();
                }

                row++;
            }

            return returnable;
        }
    }
}