namespace Scada.Comm.Devices
{
    partial class gpioForm
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
            this.gpioAmode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gpioBmode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpioCmode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gpioDmode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.gpioAval = new System.Windows.Forms.NumericUpDown();
            this.gpioBval = new System.Windows.Forms.NumericUpDown();
            this.gpioCval = new System.Windows.Forms.NumericUpDown();
            this.gpioDval = new System.Windows.Forms.NumericUpDown();
            this.cb_pulse_en_A = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_pulse_en_B = new System.Windows.Forms.CheckBox();
            this.cb_pulse_en_C = new System.Windows.Forms.CheckBox();
            this.cb_pulse_en_D = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gpioAval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpioBval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpioCval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpioDval)).BeginInit();
            this.SuspendLayout();
            // 
            // gpioAmode
            // 
            this.gpioAmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gpioAmode.FormattingEnabled = true;
            this.gpioAmode.Items.AddRange(new object[] {
            "OUTPUT",
            "INPUT"});
            this.gpioAmode.Location = new System.Drawing.Point(79, 48);
            this.gpioAmode.Name = "gpioAmode";
            this.gpioAmode.Size = new System.Drawing.Size(69, 21);
            this.gpioAmode.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "GPIO A";
            // 
            // gpioBmode
            // 
            this.gpioBmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gpioBmode.FormattingEnabled = true;
            this.gpioBmode.Items.AddRange(new object[] {
            "OUTPUT",
            "INPUT"});
            this.gpioBmode.Location = new System.Drawing.Point(79, 84);
            this.gpioBmode.Name = "gpioBmode";
            this.gpioBmode.Size = new System.Drawing.Size(69, 21);
            this.gpioBmode.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "GPIO B";
            // 
            // gpioCmode
            // 
            this.gpioCmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gpioCmode.FormattingEnabled = true;
            this.gpioCmode.Items.AddRange(new object[] {
            "OUTPUT",
            "INPUT"});
            this.gpioCmode.Location = new System.Drawing.Point(79, 120);
            this.gpioCmode.Name = "gpioCmode";
            this.gpioCmode.Size = new System.Drawing.Size(69, 21);
            this.gpioCmode.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "GPIO C";
            // 
            // gpioDmode
            // 
            this.gpioDmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gpioDmode.FormattingEnabled = true;
            this.gpioDmode.Items.AddRange(new object[] {
            "OUTPUT",
            "INPUT"});
            this.gpioDmode.Location = new System.Drawing.Point(79, 156);
            this.gpioDmode.Name = "gpioDmode";
            this.gpioDmode.Size = new System.Drawing.Size(69, 21);
            this.gpioDmode.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "GPIO D";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(198, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(110, 210);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 32;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(22, 210);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 34;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // gpioAval
            // 
            this.gpioAval.Enabled = false;
            this.gpioAval.Location = new System.Drawing.Point(173, 48);
            this.gpioAval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.gpioAval.Name = "gpioAval";
            this.gpioAval.Size = new System.Drawing.Size(73, 20);
            this.gpioAval.TabIndex = 35;
            this.gpioAval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gpioBval
            // 
            this.gpioBval.Enabled = false;
            this.gpioBval.Location = new System.Drawing.Point(173, 84);
            this.gpioBval.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gpioBval.Name = "gpioBval";
            this.gpioBval.Size = new System.Drawing.Size(73, 20);
            this.gpioBval.TabIndex = 36;
            this.gpioBval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gpioCval
            // 
            this.gpioCval.Enabled = false;
            this.gpioCval.Location = new System.Drawing.Point(173, 120);
            this.gpioCval.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gpioCval.Name = "gpioCval";
            this.gpioCval.Size = new System.Drawing.Size(73, 20);
            this.gpioCval.TabIndex = 37;
            this.gpioCval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gpioDval
            // 
            this.gpioDval.Enabled = false;
            this.gpioDval.Location = new System.Drawing.Point(173, 156);
            this.gpioDval.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gpioDval.Name = "gpioDval";
            this.gpioDval.Size = new System.Drawing.Size(73, 20);
            this.gpioDval.TabIndex = 38;
            this.gpioDval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cb_pulse_en_A
            // 
            this.cb_pulse_en_A.AutoSize = true;
            this.cb_pulse_en_A.Checked = true;
            this.cb_pulse_en_A.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_pulse_en_A.Location = new System.Drawing.Point(279, 54);
            this.cb_pulse_en_A.Name = "cb_pulse_en_A";
            this.cb_pulse_en_A.Size = new System.Drawing.Size(15, 14);
            this.cb_pulse_en_A.TabIndex = 39;
            this.cb_pulse_en_A.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cb_pulse_en_A.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Pulse Type";
            // 
            // cb_pulse_en_B
            // 
            this.cb_pulse_en_B.AutoSize = true;
            this.cb_pulse_en_B.Location = new System.Drawing.Point(279, 90);
            this.cb_pulse_en_B.Name = "cb_pulse_en_B";
            this.cb_pulse_en_B.Size = new System.Drawing.Size(15, 14);
            this.cb_pulse_en_B.TabIndex = 41;
            this.cb_pulse_en_B.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cb_pulse_en_B.UseVisualStyleBackColor = true;
            // 
            // cb_pulse_en_C
            // 
            this.cb_pulse_en_C.AutoSize = true;
            this.cb_pulse_en_C.Location = new System.Drawing.Point(279, 126);
            this.cb_pulse_en_C.Name = "cb_pulse_en_C";
            this.cb_pulse_en_C.Size = new System.Drawing.Size(15, 14);
            this.cb_pulse_en_C.TabIndex = 42;
            this.cb_pulse_en_C.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cb_pulse_en_C.UseVisualStyleBackColor = true;
            // 
            // cb_pulse_en_D
            // 
            this.cb_pulse_en_D.AutoSize = true;
            this.cb_pulse_en_D.Location = new System.Drawing.Point(279, 159);
            this.cb_pulse_en_D.Name = "cb_pulse_en_D";
            this.cb_pulse_en_D.Size = new System.Drawing.Size(15, 14);
            this.cb_pulse_en_D.TabIndex = 43;
            this.cb_pulse_en_D.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cb_pulse_en_D.UseVisualStyleBackColor = true;
            // 
            // gpioForm
            // 
            this.ClientSize = new System.Drawing.Size(328, 261);
            this.Controls.Add(this.cb_pulse_en_D);
            this.Controls.Add(this.cb_pulse_en_C);
            this.Controls.Add(this.cb_pulse_en_B);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_pulse_en_A);
            this.Controls.Add(this.gpioDval);
            this.Controls.Add(this.gpioCval);
            this.Controls.Add(this.gpioBval);
            this.Controls.Add(this.gpioAval);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gpioDmode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gpioCmode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gpioBmode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gpioAmode);
            this.Controls.Add(this.label4);
            this.Name = "gpioForm";
            this.Text = "GPIO SETTINGS";
            this.Load += new System.EventHandler(this.gpioForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gpioAval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpioBval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpioCval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpioDval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox gpioAmode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox gpioBmode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox gpioCmode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox gpioDmode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.NumericUpDown gpioAval;
        private System.Windows.Forms.NumericUpDown gpioBval;
        private System.Windows.Forms.NumericUpDown gpioCval;
        private System.Windows.Forms.NumericUpDown gpioDval;
        private System.Windows.Forms.CheckBox cb_pulse_en_A;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cb_pulse_en_B;
        private System.Windows.Forms.CheckBox cb_pulse_en_C;
        private System.Windows.Forms.CheckBox cb_pulse_en_D;
    }
}
