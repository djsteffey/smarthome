using mcp.connectors;

namespace mcp.connectors
{
    class MQTTConnector : IConnector
    {
        // events
        public event IConnector.NewDeviceEventHandler OnNewDevice;


        // methods
        public MQTTConnector()
        {

        }

        public void start()
        {
            
        }

        public void stop()
        {
            
        }
    }
}
