namespace Scada.Comm.Devices.KpWiznet
{
    partial class SerForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.serBauds = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.serDataBit = new System.Windows.Forms.ComboBox();
            this.serParity = new System.Windows.Forms.ComboBox();
            this.serStop = new System.Windows.Forms.ComboBox();
            this.serFlow = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(42, 206);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(155, 206);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Baudrate";
            // 
            // serBauds
            // 
            this.serBauds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serBauds.FormattingEnabled = true;
            this.serBauds.Items.AddRange(new object[] {
            "115200",
            "57600",
            "38400",
            "28800",
            "19200",
            "14400",
            "9600",
            "4800",
            "2400",
            "1800",
            "1200",
            "600",
            "300",
            "",
            ""});
            this.serBauds.Location = new System.Drawing.Point(123, 21);
            this.serBauds.Name = "serBauds";
            this.serBauds.Size = new System.Drawing.Size(107, 21);
            this.serBauds.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Data bit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Parity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Stop bit";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Flow Control";
            // 
            // serDataBit
            // 
            this.serDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serDataBit.FormattingEnabled = true;
            this.serDataBit.Items.AddRange(new object[] {
            "7",
            "8"});
            this.serDataBit.Location = new System.Drawing.Point(123, 55);
            this.serDataBit.Name = "serDataBit";
            this.serDataBit.Size = new System.Drawing.Size(107, 21);
            this.serDataBit.TabIndex = 23;
            // 
            // serParity
            // 
            this.serParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serParity.FormattingEnabled = true;
            this.serParity.Items.AddRange(new object[] {
            "NONE",
            "ODD",
            "EVEN"});
            this.serParity.Location = new System.Drawing.Point(123, 89);
            this.serParity.Name = "serParity";
            this.serParity.Size = new System.Drawing.Size(107, 21);
            this.serParity.TabIndex = 24;
            // 
            // serStop
            // 
            this.serStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serStop.FormattingEnabled = true;
            this.serStop.Items.AddRange(new object[] {
            "1",
            "2"});
            this.serStop.Location = new System.Drawing.Point(123, 123);
            this.serStop.Name = "serStop";
            this.serStop.Size = new System.Drawing.Size(107, 21);
            this.serStop.TabIndex = 25;
            // 
            // serFlow
            // 
            this.serFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serFlow.FormattingEnabled = true;
            this.serFlow.Items.AddRange(new object[] {
            "NONE",
            "XON/XOFF",
            "RTS/CTS",
            "RTS on TX",
            "RTS on TX (invert)"});
            this.serFlow.Location = new System.Drawing.Point(123, 157);
            this.serFlow.Name = "serFlow";
            this.serFlow.Size = new System.Drawing.Size(107, 21);
            this.serFlow.TabIndex = 26;
            // 
            // SerForm
            // 
            this.ClientSize = new System.Drawing.Size(277, 250);
            this.Controls.Add(this.serFlow);
            this.Controls.Add(this.serStop);
            this.Controls.Add(this.serParity);
            this.Controls.Add(this.serDataBit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serBauds);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "SerForm";
            this.Text = "SERIAL SETTINGS";
            this.Load += new System.EventHandler(this.SerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox serBauds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox serDataBit;
        private System.Windows.Forms.ComboBox serParity;
        private System.Windows.Forms.ComboBox serStop;
        private System.Windows.Forms.ComboBox serFlow;
    }
}
