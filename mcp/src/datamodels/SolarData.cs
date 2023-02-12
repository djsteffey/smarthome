using SQLite;

namespace mcp.datamodels
{
    [Table("SolarData")]
    class SolarData
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Time { get; set; }
        public double ProductionNow { get; set; }
        public double ProductionToday { get; set; }
        public double ProductionLastSevenDays { get; set; }
        public double ProductionLifetime { get; set; }
        public double TotalConsumptionNow { get; set; }
        public double TotalConsumptionToday { get; set; }
        public double TotalConsumptionLastSevenDays { get; set; }
        public double TotalConsumptionLifetime { get; set; }
        public double NetConsumptionNow { get; set; }
        public double NetConsumptionToday { get; set; }
        public double NetConsumptionLastSevenDays { get; set; }
        public double NetConsumptionLifetime { get; set; }

        public SolarData() : this(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public SolarData(long time, double productionNow, double productionToday, double productionLastSevenDays, double productionLifetime, double totalConsumptionNow, double totalConsumptionToday, double totalConsumptionLastSevenDays, double totalConsumptionLifetime, double netConsumptionNow, double netConsumptionToday, double netConsumptionLastSevenDays, double netConsumptionLifetime)
        {
            this.Id = -1;
            this.Time = time;
            this.ProductionNow = productionNow;
            this.ProductionToday = productionToday;
            this.ProductionLastSevenDays = productionLastSevenDays;
            this.ProductionLifetime = productionLifetime;
            this.TotalConsumptionNow = totalConsumptionNow;
            this.TotalConsumptionToday = totalConsumptionToday;
            this.TotalConsumptionLastSevenDays = totalConsumptionLastSevenDays;
            this.TotalConsumptionLifetime = totalConsumptionLifetime;
            this.NetConsumptionNow = netConsumptionNow;
            this.NetConsumptionToday = netConsumptionToday;
            this.NetConsumptionLastSevenDays = netConsumptionLastSevenDays;
            this.NetConsumptionLifetime = netConsumptionLifetime;
        }
    }
}
