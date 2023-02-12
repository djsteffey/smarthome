using commonlib;
using mcp.connectors;
using mcp.database;
using mcp.datamodels;

namespace mcp
{
    class MCP
    {
        // variables
        private bool m_running;
        private List<IConnector> m_connectors;
        private List<Device> m_devices;

        // methods
        public MCP()
        {
            // init a SQL database
            Database.init(typeof(SQLiteDatabase));

            // run a solar thread
            /*
            new Thread(async ()=>{
                // build the connection to enlighten
                Enlighten enlighten = new Enlighten("djsteffey@gmail.com", "SnooganS1278!!");

                // loop forever
                while (true)
                {
                    // get the data
                    SolarData data = await enlighten.getSolarDataAsync();

                    // ensure valid
                    if (data != null)
                    {
                        // log it
                        Log.log("Enlighten", String.Format("{0}\t{1}\t{2}\t{3}", Utils.convertTimestampToDateTime(data.Time), data.ProductionNow, data.TotalConsumptionNow, data.NetConsumptionNow));

                        // send to the database
                        Database.Instance.putSolarData(data);
                    }

                    // sleep the config seconds
                    Thread.Sleep(Config.SolarToDatabaseRate);
                }
            }).Start();*/
            
            // init
            this.m_running = false;
            this.m_connectors = new List<IConnector>();
            this.m_devices = new List<Device>();
        }

        public void run()
        {
            // check if already running
            if (this.m_running)
            {
                return;
            }

            // flag running
            this.m_running = true;

            // add connectors
            ZWaveConnector zwave = new ZWaveConnector("COM3");
            this.m_connectors.Add(zwave);
            this.m_connectors.Add(new MQTTConnector());

            // start each connector
            foreach (IConnector connector in this.m_connectors)
            {
                connector.OnNewDevice += IConnector_OnNewDevice;
                connector.start();
            }

            // add
 
            // loop while running
            while (this.m_running)
            {
                Thread.Sleep(250);
            }
        }

        private void IConnector_OnNewDevice(Device device)
        {
            // add to our list of devices
            this.m_devices.Add(device);
        }
    }
}
