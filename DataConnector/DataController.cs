using System;

namespace DataConnectorNS
{
    public class DataController
    {
        private DataConnector connector = DataConnector.Instance;

        public void TestConnection()
        {
            connector = new DataConnector();
            Console.WriteLine(connector.
        }
    }
}
