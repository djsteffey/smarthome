namespace commonlib
{
    class DeviceProperty
    {
        public enum EValueType
        {
            BOOL, INT, FLOAT, STRING
        }

        // variables
        private string m_name;
        protected EValueType m_valueType;
        private bool m_readOnly;
        protected object m_value;

        // properties
        public bool AsBool
        {
            get { return (bool)this.m_value; }
        }

        public int AsInt
        {
            get { return (int)this.m_value; }
        }

        public float AsFloat
        {
            get { return (float)this.m_value; }
        }

        public string AsString
        {
            get { return m_name; }
        }

        public EValueType ValueType
        {
            get { return this.m_valueType; }
        }


        // methods
        public DeviceProperty(string name, EValueType type, bool readOnly) : this(name, type, readOnly, null)
        {

        }

        public DeviceProperty(string name, EValueType type, bool readOnly, object value)
        {
            this.m_name = name;
            this.m_valueType = type;
            this.m_readOnly = readOnly;
            this.m_value = value;
        }
    }
}
