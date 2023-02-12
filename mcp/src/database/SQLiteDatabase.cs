using mcp.datamodels;
using SQLite;

namespace mcp.database
{
    class SQLiteDatabase : Database
    {
        // variables
        private SQLiteConnection m_connection;

        // methods
        public SQLiteDatabase()
        {
            // create the connection
            this.m_connection = new SQLiteConnection("data.db");

            // build the tables (if needed)
            this.m_connection.CreateTable<SolarData>();
        }

        public override bool putSolarData(SolarData data)
        {
            return this.m_connection.Insert(data) == 1;
        }
    }
}
