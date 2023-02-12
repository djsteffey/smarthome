using mcp.datamodels;

namespace mcp.database
{
    class MemoryDatabase : Database
    {
        // variables
        private List<SolarData> m_solarData;

        // methods
        private MemoryDatabase() : base()
        {
            this.m_solarData = new List<SolarData>();
        }

        public override bool putSolarData(SolarData data)
        {
            this.m_solarData.Add(data);
            return true;
        }
    }
}
