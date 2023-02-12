using common;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ZWave;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace smarthome_winforms
{
    public partial class Form1 : Form
    {
        private ZWaveService m_service;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.m_service = new ZWaveService("COM6");
            this.m_service.OnLog += (tag, message) =>
            {
                this.log(tag, message);
            };
            this.m_service.start();
        }

        private void log(string tag, string message)
        {
            if (this.m_listbox_zwaveServiceLog.InvokeRequired)
            {
                this.m_listbox_zwaveServiceLog.Invoke((MethodInvoker)delegate
                {
                    this.log(tag, message);
                });
            }
            else
            {
                this.m_listbox_zwaveServiceLog.Items.Add(DateTime.Now.Ticks.ToString() + " " + tag + ": " + message);
                this.m_listbox_zwaveServiceLog.SelectedIndex = this.m_listbox_zwaveServiceLog.Items.Count - 1;
                this.m_listbox_zwaveServiceLog.SelectedIndex = -1;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.m_service.stop();
        }

        private void m_button_zwave_hardReset_Click(object sender, EventArgs e)
        {
            this.m_service.setTemperature(70);
        }

        private async void m_button_zwave_addNodeStart_Click(object sender, EventArgs e)
        {
            await this.m_service.startAddNode();
        }

        private async void m_button_zwave_addNodeStop_Click(object sender, EventArgs e)
        {
            await this.m_service.stopAddNode();
        }

        private async void m_button_zwave_excludeNodeStart_Click(object sender, EventArgs e)
        {
            await this.m_service.startRemoveNode();
        }

        private async void m_button_zwave_excludeNodeStop_Click(object sender, EventArgs e)
        {
            await this.m_service.stopRemoveNode();
        }

        private async void m_button_zwave_getDevices_Click(object sender, EventArgs e)
        {
        }
    }
}