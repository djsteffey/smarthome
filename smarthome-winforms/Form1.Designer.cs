namespace smarthome_winforms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_listbox_zwaveDevices = new System.Windows.Forms.ListBox();
            this.m_button_zwave_excludeNodeStop = new System.Windows.Forms.Button();
            this.m_button_zwave_excludeNodeStart = new System.Windows.Forms.Button();
            this.m_button_zwave_addNodeStop = new System.Windows.Forms.Button();
            this.m_button_zwave_addNodeStart = new System.Windows.Forms.Button();
            this.m_button_zwave_hardReset = new System.Windows.Forms.Button();
            this.m_listbox_zwaveServiceLog = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabControl
            // 
            this.m_tabControl.Controls.Add(this.tabPage1);
            this.m_tabControl.Controls.Add(this.tabPage2);
            this.m_tabControl.Location = new System.Drawing.Point(12, 12);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.Size = new System.Drawing.Size(1005, 517);
            this.m_tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_listbox_zwaveDevices);
            this.tabPage1.Controls.Add(this.m_button_zwave_excludeNodeStop);
            this.tabPage1.Controls.Add(this.m_button_zwave_excludeNodeStart);
            this.tabPage1.Controls.Add(this.m_button_zwave_addNodeStop);
            this.tabPage1.Controls.Add(this.m_button_zwave_addNodeStart);
            this.tabPage1.Controls.Add(this.m_button_zwave_hardReset);
            this.tabPage1.Controls.Add(this.m_listbox_zwaveServiceLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(997, 489);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_listbox_zwaveDevices
            // 
            this.m_listbox_zwaveDevices.FormattingEnabled = true;
            this.m_listbox_zwaveDevices.ItemHeight = 15;
            this.m_listbox_zwaveDevices.Location = new System.Drawing.Point(743, 6);
            this.m_listbox_zwaveDevices.Name = "m_listbox_zwaveDevices";
            this.m_listbox_zwaveDevices.Size = new System.Drawing.Size(248, 424);
            this.m_listbox_zwaveDevices.TabIndex = 6;
            // 
            // m_button_zwave_excludeNodeStop
            // 
            this.m_button_zwave_excludeNodeStop.Location = new System.Drawing.Point(330, 436);
            this.m_button_zwave_excludeNodeStop.Name = "m_button_zwave_excludeNodeStop";
            this.m_button_zwave_excludeNodeStop.Size = new System.Drawing.Size(75, 23);
            this.m_button_zwave_excludeNodeStop.TabIndex = 5;
            this.m_button_zwave_excludeNodeStop.Text = "stopexclude";
            this.m_button_zwave_excludeNodeStop.UseVisualStyleBackColor = true;
            this.m_button_zwave_excludeNodeStop.Click += new System.EventHandler(this.m_button_zwave_excludeNodeStop_Click);
            // 
            // m_button_zwave_excludeNodeStart
            // 
            this.m_button_zwave_excludeNodeStart.Location = new System.Drawing.Point(249, 436);
            this.m_button_zwave_excludeNodeStart.Name = "m_button_zwave_excludeNodeStart";
            this.m_button_zwave_excludeNodeStart.Size = new System.Drawing.Size(75, 23);
            this.m_button_zwave_excludeNodeStart.TabIndex = 4;
            this.m_button_zwave_excludeNodeStart.Text = "startexclude";
            this.m_button_zwave_excludeNodeStart.UseVisualStyleBackColor = true;
            this.m_button_zwave_excludeNodeStart.Click += new System.EventHandler(this.m_button_zwave_excludeNodeStart_Click);
            // 
            // m_button_zwave_addNodeStop
            // 
            this.m_button_zwave_addNodeStop.Location = new System.Drawing.Point(168, 436);
            this.m_button_zwave_addNodeStop.Name = "m_button_zwave_addNodeStop";
            this.m_button_zwave_addNodeStop.Size = new System.Drawing.Size(75, 23);
            this.m_button_zwave_addNodeStop.TabIndex = 3;
            this.m_button_zwave_addNodeStop.Text = "stop add";
            this.m_button_zwave_addNodeStop.UseVisualStyleBackColor = true;
            this.m_button_zwave_addNodeStop.Click += new System.EventHandler(this.m_button_zwave_addNodeStop_Click);
            // 
            // m_button_zwave_addNodeStart
            // 
            this.m_button_zwave_addNodeStart.Location = new System.Drawing.Point(87, 436);
            this.m_button_zwave_addNodeStart.Name = "m_button_zwave_addNodeStart";
            this.m_button_zwave_addNodeStart.Size = new System.Drawing.Size(75, 23);
            this.m_button_zwave_addNodeStart.TabIndex = 2;
            this.m_button_zwave_addNodeStart.Text = "start add";
            this.m_button_zwave_addNodeStart.UseVisualStyleBackColor = true;
            this.m_button_zwave_addNodeStart.Click += new System.EventHandler(this.m_button_zwave_addNodeStart_Click);
            // 
            // m_button_zwave_hardReset
            // 
            this.m_button_zwave_hardReset.Location = new System.Drawing.Point(6, 436);
            this.m_button_zwave_hardReset.Name = "m_button_zwave_hardReset";
            this.m_button_zwave_hardReset.Size = new System.Drawing.Size(75, 23);
            this.m_button_zwave_hardReset.TabIndex = 1;
            this.m_button_zwave_hardReset.Text = "hard reset";
            this.m_button_zwave_hardReset.UseVisualStyleBackColor = true;
            this.m_button_zwave_hardReset.Click += new System.EventHandler(this.m_button_zwave_hardReset_Click);
            // 
            // m_listbox_zwaveServiceLog
            // 
            this.m_listbox_zwaveServiceLog.FormattingEnabled = true;
            this.m_listbox_zwaveServiceLog.HorizontalScrollbar = true;
            this.m_listbox_zwaveServiceLog.ItemHeight = 15;
            this.m_listbox_zwaveServiceLog.Location = new System.Drawing.Point(6, 6);
            this.m_listbox_zwaveServiceLog.Name = "m_listbox_zwaveServiceLog";
            this.m_listbox_zwaveServiceLog.Size = new System.Drawing.Size(731, 424);
            this.m_listbox_zwaveServiceLog.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(997, 489);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 541);
            this.Controls.Add(this.m_tabControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.m_tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl m_tabControl;
        private TabPage tabPage1;
        private ListBox m_listbox_zwaveServiceLog;
        private TabPage tabPage2;
        private Button m_button_zwave_excludeNodeStop;
        private Button m_button_zwave_excludeNodeStart;
        private Button m_button_zwave_addNodeStop;
        private Button m_button_zwave_addNodeStart;
        private Button m_button_zwave_hardReset;
        private ListBox m_listbox_zwaveDevices;
    }
}