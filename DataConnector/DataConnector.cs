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

        private static int GetRowCount(string tableName, string columnName, dynamic value)
        {
            string sql = "SELECT COUNT(*) FROM " + tableName + " WHERE " + columnName + " = '" + value + "'";

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

            Connection.Close();
            return returnable;
        }

        public static string[,] GetDataWithWhere(string query, string tableName, string columnName, dynamic value)
        {
            int rowCount = GetRowCount(tableName, columnName, value);

            Query = query;

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

            Connection.Close();
            return returnable;
        }

        public static int InsertData(string tableName, Dictionary<string, dynamic> columnData)
        {
            string columns = string.Empty;
            string data = string.Empty;
            int pairCount = 0;

            foreach (KeyValuePair<string, dynamic> pair in columnData)
            {
                pairCount++;

                columns += "[" + pair.Key + "]";

                dynamic value = pair.Value;


                if (value is decimal)
                {
                    if (((decimal)value).ToString().Contains(","))
                        value = ((decimal)value).ToString().Replace(',', '.');
                }

                data += "'" + value + "'";

                if (pairCount != columnData.Count)
                {
                    columns += ", ";
                    data += ", ";
                }
            }

            Query = string.Format("INSERT INTO " + tableName + "({0}) " +
                                    "VALUES({1})", columns, data);

            Command = new SqlCommand(Query, Connection);
            OpenConnection();

            return Command.ExecuteNonQuery();
        }
    }
}