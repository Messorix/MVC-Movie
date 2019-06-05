using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataConnectorNS
{
    public class DataConnector
    {
        #region Singleton
        private DataConnector() { }

        private static DataConnector instance = new DataConnector();

        public static DataConnector Instance {
            get {
                return instance;
            }
        }
        #endregion

        private static SqlCommand command { get; set; }

        public static ConnectionState TestConnection()
        {
            return command.Connection.State;
        }

        private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}