using commonlib;
using ZWaveLib;

namespace mcp.connectors
{
    class ZWaveConnector : IConnector
    {
        // variables
        protected bool m_running;
        public ZWaveController m_controller;
        private List<ZWaveDevice> m_devices;

        // events
        public event IConnector.NewDeviceEventHandler OnNewDevice;

        // methods
        public ZWaveConnector(string serialPort)
        {
            // flag not running
            this.m_running = false;

            // create the controller
            this.m_controller = new ZWaveController(serialPort);

            // device list
            this.m_devices = new List<ZWaveDevice>();

            // register the handlers
            this.m_controller.ControllerStatusChanged += controller_ControllerStatusChanged;
            this.m_controller.DiscoveryProgress += controller_DiscoveryProgress;
            this.m_controller.HealProgress += controller_HealProgress;
            this.m_controller.NodeOperationProgress += controller_NodeOperationProgress;
            this.m_controller.NodeUpdated += controller_NodeUpdated;
        }

        public void start()
        {
            // check if already running
            if (this.m_running)
            {
                return;
            }

            // flag running
            this.m_running = true;

            // connect to the controller
            this.m_controller.Connect();
        }

        public void stop()
        {
            // check if running
            if (this.m_running == false)
            {
                return;
            }

            // flag not running
            this.m_running = false;

            // disconnect controller
            this.m_controller.Disconnect();

            
        }

        public void addNode()
        {
            this.m_controller.BeginNodeAdd();
        }

        private void controller_ControllerStatusChanged(object sender, ControllerStatusEventArgs args)
        {
            // get handle to the controller
            ZWaveController controller = sender as ZWaveController;

            // log
            Console.WriteLine(this.GetType().ToString() + ": controller status changed: " + args.Status);
            
            // switch on the status
            switch (args.Status)
            {
                case ControllerStatus.Connected:
                    {
                        // initialize controller
                        this.m_controller.Initialize();
                    } break;
                case ControllerStatus.Disconnected:
                    {

                    } break;
                case ControllerStatus.Error:
                    {

                    } break;
                case ControllerStatus.Initializing:
                    {

                    } break;
                case ControllerStatus.Ready:
                    {
                        // discover nodes
                        this.m_controller.Discovery();
                    } break;
                default:
                    {

                    } break;
            }
        }

        private void controller_DiscoveryProgress(object sender, DiscoveryProgressEventArgs args)
        {
            // get handle to the controller
            ZWaveController controller = sender as ZWaveController;

            // log
            Console.WriteLine(this.GetType().ToString() + ": controller discovery progress: " + args.Status);
            
            // switch
            switch (args.Status)
            {
                case DiscoveryStatus.DiscoveryStart:
                    {
                        
                    } break;
                case DiscoveryStatus.DiscoveryEnd:
                    {
                        Console.WriteLine("*****begin node add");
                        this.m_controller.BeginNodeAdd();
                    }
                    break;
                case DiscoveryStatus.DiscoveryError:
                    {

                    } break;
                default:
                    {

                    } break;
            }
        }

        private void controller_HealProgress(object sender, HealProgressEventArgs args)
        {
            // get handle to the controller
            ZWaveController controller = sender as ZWaveController;

            // log
            Console.WriteLine(this.GetType().ToString() + ": controller heal progress: " + args.Status);

            // switch
            switch (args.Status)
            {
                case HealStatus.HealStart:
                    {

                    } break;
                case HealStatus.HealEnd:
                    {

                    } break;
                case HealStatus.HealError:
                    {

                    } break;
                default:
                    {

                    } break;
            }
        }

        private void controller_NodeOperationProgress(object sender, NodeOperationProgressEventArgs args)
        {
            // get handle to the controller
            ZWaveController controller = sender as ZWaveController;

            // log
            Console.WriteLine(this.GetType().ToString() + ": controller node operation: " + args.NodeId + "->" + args.Status);

            // switch
            switch (args.Status)
            {
                case NodeQueryStatus.NodeAdded:
                    {
                        this.onNodeAdded(args.NodeId);

                        Console.WriteLine("end node add");
                        this.m_controller.StopNodeAdd();

                    } break;
                case NodeQueryStatus.Error:
                    {
                        Console.WriteLine("*** ERROR ***");
                    } break;
                default:
                    {

                    } break;
            }
        }

        private void controller_NodeUpdated(object sender, NodeUpdatedEventArgs args)
        {
            // get handle to the controller
            ZWaveController controller = sender as ZWaveController;

            // log
            Console.WriteLine("{0}: node updated: {1}->{2}->{3}", this.GetType().ToString(), args.NodeId, args.Event.Parameter, args.Event.Value);
        }

        private void onNodeAdded(byte id)
        {
            // create a new zwave device
            ZWaveDevice device = new ZWaveDevice(Guid.NewGuid(), "zwave-device-" + id, id);

            // add to list
            this.m_devices.Add(device);

            // inform the event
            if (this.OnNewDevice != null)
            {
                this.OnNewDevice(device);
            }
        }
    }
}
