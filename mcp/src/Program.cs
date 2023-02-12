using mcp.connectors;

namespace mcp.src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MCP mcp = new MCP();
            mcp.run();
        }
    }
}