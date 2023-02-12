using mcp.datamodels;

namespace mcp.database
{
    abstract class Database
    {

        // variables
        private static Database s_instance;

        // properties
        public static Database Instance
        {
            get
            {
                return s_instance;
            }
        }

        // methods
        public static bool init(Type type)
        {
            // ensure the type is a subclass of database
            if (type.IsSubclassOf(typeof(Database)) == false)
            {
                return false;
            }

            // create the static instance
            Database.s_instance = (Database)Activator.CreateInstance(type);

            // done
            return true;
        }

        protected Database()
        {

        }

        public abstract bool putSolarData(SolarData data);
    }
}
