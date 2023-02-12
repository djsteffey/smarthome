using System.Data;

namespace commonlib
{
    class Device
    {
        // variables
        private Guid m_guid;
        private string m_name;
        private Dictionary<string, DeviceProperty> m_properties;

        // methods
        public Device(Guid guid, string name)
        {
            this.m_guid = guid;
            this.m_name = name;
            this.m_properties = new Dictionary<string, DeviceProperty>();
        }

        public void addProperty(string name, DeviceProperty.EValueType type, bool readOnly)
        {
            this.m_properties[name] = new DeviceProperty(name, type, readOnly);
        }
    }

    class ZWaveDevice : Device
    {
        // variables
        private byte m_zWaveId;


        // methods
        public ZWaveDevice(Guid guid, string name, byte zWaveId) : base(guid, name)
        {
            this.m_zWaveId = zWaveId;
        }
    }
}
