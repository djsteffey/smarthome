
using System.Text;
using ZWave;
using ZWave.Channel;
using ZWave.CommandClasses;
using ZWave.Devices.Vision;

namespace common
{
    public class ZWaveDevice : IDevice
    {

    }

    public class AA : TextWriter
    {
        private ZWaveService zWaveServicezwaveService;
        private string current;
        public AA(ZWaveService service) { this.zWaveServicezwaveService = service; }
        public override Encoding Encoding => Encoding.ASCII;
        public override void Write(char value)
        {
            this.current += value;
            if (value== '\n')
            {
                this.zWaveServicezwaveService.log(this.current);
                this.current = "";
            }
            base.Write(value);
        }
    }

    public class ZWaveService
    {
        public delegate void LogCallback(string tag, string message);

        // variables
        private bool m_running;
        private ZWaveController m_controller;


        // events
        public event LogCallback OnLog;

        // methods
        public ZWaveService(string serialPort)
        {
            // flag not running
            this.m_running = false;

            // events
            this.OnLog = null;

            // create the controller
            this.m_controller = new ZWaveController(serialPort);
            this.m_controller.ChannelClosed += onControllerChannelClosed;
            this.m_controller.Error += onControllerError;
            this.m_controller.NodesNetworkChanged += onControllerNodesNetworkChanged;
            this.m_controller.Channel.Closed += onControllerChannelClosed;
            this.m_controller.Channel.Error += Channel_Error;
            this.m_controller.Channel.NodeEventReceived += Channel_NodeEventReceived;
            this.m_controller.Channel.NodeUpdateReceived += Channel_NodeUpdateReceived;
            this.m_controller.Channel.Log = new AA(this);
        }

        private void Channel_NodeUpdateReceived(object sender, ZWave.Channel.NodeUpdateEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Channel_NodeEventReceived(object sender, ZWave.Channel.NodeEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Channel_Error(object sender, ZWave.ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void start()
        {
            // log
            this.log("Starting ZWave Service");

            // check if already running
            if (this.m_running)
            {
                // log
                this.log("ZWave Service is already running");
                return;
            }

            // flag running
            this.m_running = true;

            // log
            this.log("Opening the controller");

            // start zwave driver
            this.m_controller.Open();
            this.getNodes();
        }

        public void stop()
        {
            // log
            this.log("Stopping ZWave Service");

            // check if running
            if (this.m_running == false)
            {
                // log
                this.log("ZWave Service is not running");
                return;
            }

            // flag not running
            this.m_running = false;

            // log
            this.log("Closing the controller");

            // disconnect controller
            this.m_controller.Close();
        }

        public async Task<bool> startAddNode()
        {
            bool result = await this.m_controller.StartAddingNodesToNetwork();
             this.log("startAddNode: " + result);
            return result;
        }

        public async Task<bool> stopAddNode()
        {
            bool result = await this.m_controller.StopAddingNodesToNetwork();
            this.log("stopAddNode: " + result);
            return result;
        }

        public async Task<bool> startRemoveNode()
        {
            bool result = await this.m_controller.StartRemoveNodeFromNetwork();
            this.log("startRemoveNode: " + result);
            return result;
        }

        public async Task<bool> stopRemoveNode()
        {
            bool result = await this.m_controller.StopRemoveNodeFromNetwork();
            this.log("stopRemoveNode: " + result);
            return result;
        }

        public void log(string message)
        {
            if (this.OnLog != null)
            {
                this.OnLog(this.GetType().ToString(), message);
            }
        }

        public async void setTemperature(int degrees)
        {
            NodeCollection allNodes = await this.m_controller.GetNodes();
            Node node = allNodes[4];
            
            ThermostatSetpoint cc = node.GetCommandClass<ThermostatSetpoint>();
            await cc.Set(ThermostatSetpointType.Heating, degrees);
            int y = 0;
        }

        private async void getNodes()
        {
            this.log("getting nodes");
            var nodes = await this.m_controller.GetNodes();
            this.log("iterating nodes");
            foreach (var node in nodes)
            {
                this.log(node.ToString());
                try
                {
                    var npi = await node.GetProtocolInfo();
                    this.log(node.NodeID + ": " + npi.ToString());
                    var v = await node.GetSupportedCommandClasses();
                    this.log(node.NodeID + ": " + v.ToString());
                    int y = 0;
                }
                catch (Exception ex)
                {
                    this.log(ex.ToString());
                }
            }
        }

        private void onControllerNodesNetworkChanged(object sender, NodesNetworkChangeEventArgs e)
        {
            this.log("onControllerNodesNetworkChanged");
        }

        private void onControllerError(object sender, ZWave.ErrorEventArgs e)
        {
            this.log("onControllerError: " + e.Error.Message);
        }

        private void onControllerChannelClosed(object sender, EventArgs e)
        {
            this.log("onControllerError");
        }
    }
}
