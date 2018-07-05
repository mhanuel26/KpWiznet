namespace Scada.Comm.Devices.KpWiznet
{
    partial class WiznetConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEditSerialSettings = new System.Windows.Forms.Button();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.CfgPort = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIPaddr = new System.Windows.Forms.TextBox();
            this.bttnGPIO = new System.Windows.Forms.Button();
            this.enPolling = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.txtMask = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGateway = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radioDHCP = new System.Windows.Forms.RadioButton();
            this.radioStatic = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.devicesMAC = new System.Windows.Forms.ListBox();
            this.btnDisco = new System.Windows.Forms.Button();
            this.textPName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textFirmware = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGwMode = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEditSerialSettings
            // 
            this.btnEditSerialSettings.Location = new System.Drawing.Point(12, 258);
            this.btnEditSerialSettings.Name = "btnEditSerialSettings";
            this.btnEditSerialSettings.Size = new System.Drawing.Size(102, 23);
            this.btnEditSerialSettings.TabIndex = 11;
            this.btnEditSerialSettings.Text = "Serial Settings";
            this.btnEditSerialSettings.UseVisualStyleBackColor = true;
            this.btnEditSerialSettings.Click += new System.EventHandler(this.btnEditSerialSettings_Click);
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(237, 25);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(101, 20);
            this.numPort.TabIndex = 3;
            this.numPort.Value = new decimal(new int[] {
            50001,
            0,
            0,
            0});
            // 
            // CfgPort
            // 
            this.CfgPort.AutoSize = true;
            this.CfgPort.Location = new System.Drawing.Point(234, 9);
            this.CfgPort.Name = "CfgPort";
            this.CfgPort.Size = new System.Drawing.Size(59, 13);
            this.CfgPort.TabIndex = 2;
            this.CfgPort.Text = "Config Port";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(12, 41);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(46, 13);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Devices";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(127, 334);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(237, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Interface Setup";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "IP Address";
            // 
            // txtIPaddr
            // 
            this.txtIPaddr.Location = new System.Drawing.Point(8, 33);
            this.txtIPaddr.Name = "txtIPaddr";
            this.txtIPaddr.Size = new System.Drawing.Size(121, 20);
            this.txtIPaddr.TabIndex = 16;
            // 
            // bttnGPIO
            // 
            this.bttnGPIO.Location = new System.Drawing.Point(12, 287);
            this.bttnGPIO.Name = "bttnGPIO";
            this.bttnGPIO.Size = new System.Drawing.Size(102, 23);
            this.bttnGPIO.TabIndex = 17;
            this.bttnGPIO.Text = "GPIO Settings";
            this.bttnGPIO.UseVisualStyleBackColor = true;
            this.bttnGPIO.Click += new System.EventHandler(this.bttnGPIO_Click);
            // 
            // enPolling
            // 
            this.enPolling.AutoSize = true;
            this.enPolling.Checked = true;
            this.enPolling.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enPolling.Location = new System.Drawing.Point(127, 264);
            this.enPolling.Name = "enPolling";
            this.enPolling.Size = new System.Drawing.Size(93, 17);
            this.enPolling.TabIndex = 18;
            this.enPolling.Text = "Enable Polling";
            this.enPolling.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(17, 334);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 19;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // txtMask
            // 
            this.txtMask.Location = new System.Drawing.Point(8, 72);
            this.txtMask.Name = "txtMask";
            this.txtMask.Size = new System.Drawing.Size(121, 20);
            this.txtMask.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Subnet mask";
            // 
            // txtGateway
            // 
            this.txtGateway.Location = new System.Drawing.Point(8, 109);
            this.txtGateway.Name = "txtGateway";
            this.txtGateway.Size = new System.Drawing.Size(121, 20);
            this.txtGateway.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Gateway Address";
            // 
            // radioDHCP
            // 
            this.radioDHCP.AutoSize = true;
            this.radioDHCP.Checked = true;
            this.radioDHCP.Location = new System.Drawing.Point(74, 19);
            this.radioDHCP.Name = "radioDHCP";
            this.radioDHCP.Size = new System.Drawing.Size(55, 17);
            this.radioDHCP.TabIndex = 24;
            this.radioDHCP.TabStop = true;
            this.radioDHCP.Text = "DHCP";
            this.radioDHCP.UseVisualStyleBackColor = true;
            this.radioDHCP.CheckedChanged += new System.EventHandler(this.radioDHCP_CheckedChanged);
            // 
            // radioStatic
            // 
            this.radioStatic.AutoSize = true;
            this.radioStatic.Location = new System.Drawing.Point(16, 19);
            this.radioStatic.Name = "radioStatic";
            this.radioStatic.Size = new System.Drawing.Size(52, 17);
            this.radioStatic.TabIndex = 25;
            this.radioStatic.Text = "Static";
            this.radioStatic.UseVisualStyleBackColor = true;
            this.radioStatic.CheckedChanged += new System.EventHandler(this.radioStatic_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioStatic);
            this.groupBox1.Controls.Add(this.radioDHCP);
            this.groupBox1.Location = new System.Drawing.Point(200, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 46);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP allocation";
            // 
            // devicesMAC
            // 
            this.devicesMAC.FormattingEnabled = true;
            this.devicesMAC.Location = new System.Drawing.Point(12, 60);
            this.devicesMAC.Name = "devicesMAC";
            this.devicesMAC.Size = new System.Drawing.Size(164, 95);
            this.devicesMAC.TabIndex = 28;
            // 
            // btnDisco
            // 
            this.btnDisco.Location = new System.Drawing.Point(12, 15);
            this.btnDisco.Name = "btnDisco";
            this.btnDisco.Size = new System.Drawing.Size(75, 23);
            this.btnDisco.TabIndex = 29;
            this.btnDisco.Text = "Discovery";
            this.btnDisco.UseVisualStyleBackColor = true;
            this.btnDisco.Click += new System.EventHandler(this.btnDisco_Click);
            // 
            // textPName
            // 
            this.textPName.Location = new System.Drawing.Point(69, 14);
            this.textPName.Name = "textPName";
            this.textPName.Size = new System.Drawing.Size(89, 20);
            this.textPName.TabIndex = 30;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textFirmware);
            this.groupBox2.Controls.Add(this.textPName);
            this.groupBox2.Location = new System.Drawing.Point(12, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 68);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Product Info";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Firmware";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Name";
            // 
            // textFirmware
            // 
            this.textFirmware.Enabled = false;
            this.textFirmware.Location = new System.Drawing.Point(69, 37);
            this.textFirmware.Name = "textFirmware";
            this.textFirmware.Size = new System.Drawing.Size(89, 20);
            this.textFirmware.TabIndex = 31;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtGateway);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtMask);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtIPaddr);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(200, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(138, 135);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Network Settings";
            // 
            // btnGwMode
            // 
            this.btnGwMode.Location = new System.Drawing.Point(127, 287);
            this.btnGwMode.Name = "btnGwMode";
            this.btnGwMode.Size = new System.Drawing.Size(75, 23);
            this.btnGwMode.TabIndex = 33;
            this.btnGwMode.Text = "GW Mode";
            this.btnGwMode.UseVisualStyleBackColor = true;
            this.btnGwMode.Click += new System.EventHandler(this.BtnGwMode_Click);
            // 
            // WiznetConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(350, 368);
            this.Controls.Add(this.btnGwMode);
            this.Controls.Add(this.btnDisco);
            this.Controls.Add(this.devicesMAC);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.enPolling);
            this.Controls.Add(this.bttnGPIO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numPort);
            this.Controls.Add(this.CfgPort);
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.btnEditSerialSettings);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WiznetConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wiznet Test";
            this.Load += new System.EventHandler(this.WiznetConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEditSerialSettings;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label CfgPort;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIPaddr;
        private System.Windows.Forms.Button bttnGPIO;
        private System.Windows.Forms.CheckBox enPolling;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtMask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGateway;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioDHCP;
        private System.Windows.Forms.RadioButton radioStatic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox devicesMAC;
        private System.Windows.Forms.Button btnDisco;
        private System.Windows.Forms.TextBox textPName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textFirmware;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGwMode;
    }
}