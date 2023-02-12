using commonlib;

namespace mcp.connectors
{
    interface IConnector
    {
        // event handler delegates
        delegate void NewDeviceEventHandler(Device device);

        // events
        event NewDeviceEventHandler OnNewDevice;

        // methods
        void start();
        void stop();
    }
}
