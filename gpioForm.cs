using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Scada.UI;

namespace Scada.Comm.Devices
{
    public partial class gpioForm : Form
    {
        private gpioConfig config;
        private string configFileName; // configuration file name
        private AppDirs appDirs;       // the application directory
        private int kpNum;             // number of customizable control unit

        public gpioForm()
        {
            InitializeComponent();

            config = new gpioConfig();
        }

        private void ModeSelect_SelectedIndexChanged(object sender,
            System.EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.Name.Equals("gpioAmode"))
            {
                string NewGpiomode = (string)gpioAmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
                gpioAval.Enabled = NewGpiomode.Equals("INPUT") ? false : true;
                return;
            }
            if (comboBox.Name.Equals("gpioBmode"))
            {
                string NewGpiomode = (string)gpioBmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
                gpioBval.Enabled = NewGpiomode.Equals("INPUT") ? false : true;
                return;
            }
            if (comboBox.Name.Equals("gpioCmode"))
            {
                string NewGpiomode = (string)gpioCmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
                gpioCval.Enabled = NewGpiomode.Equals("INPUT") ? false : true;
                return;
            }
            if (comboBox.Name.Equals("gpioDmode"))
            {
                string NewGpiomode = (string)gpioDmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
                gpioDval.Enabled = NewGpiomode.Equals("INPUT") ? false : true;
                return;
            }
        }

        /// <summary>
        /// Set the controls according to the configuration
        /// </summary>
        private void ValueToControls()
        {
            string CurrentGpioAmode = (string)gpioAmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
            if (CurrentGpioAmode.Equals("INPUT"))
            {
                gpioAval.Value = config.GpioA.Value;
            }
            string CurrentGpioBmode = (string)gpioBmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
            if (CurrentGpioBmode.Equals("INPUT"))
            {
                gpioBval.Value = config.GpioB.Value;
            }
            string CurrentGpioCmode = (string)gpioCmode.GetSelectedItem(
                new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
            if (CurrentGpioCmode.Equals("INPUT"))
            {
                gpioCval.Value = config.GpioC.Value;
            }
            string CurrentGpioDmode = (string)gpioDmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
            if (CurrentGpioDmode.Equals("INPUT"))
            {
                gpioDval.Value = config.GpioD.Value;
            }
        }

        /// <summary>
        /// Set the controls according to the configuration
        /// </summary>
        private void ConfigToControls()
        {
            // gpio modes - INPUT or OUTPUT
            gpioAmode.SetSelectedItem(config.GpioA.Mode, new Dictionary<string, int> { { "OUTPUT", 0 }, { "INPUT", 1 } }, 0);
            gpioBmode.SetSelectedItem(config.GpioB.Mode, new Dictionary<string, int> { { "OUTPUT", 0 }, { "INPUT", 1 } }, 0);
            gpioCmode.SetSelectedItem(config.GpioC.Mode, new Dictionary<string, int> { { "OUTPUT", 0 }, { "INPUT", 1 } }, 0);
            gpioDmode.SetSelectedItem(config.GpioD.Mode, new Dictionary<string, int> { { "OUTPUT", 0 }, { "INPUT", 1 } }, 0);
            // gpio values
            gpioAval.Value = config.GpioA.Value;
            gpioBval.Value = config.GpioB.Value;
            gpioCval.Value = config.GpioC.Value;
            gpioDval.Value = config.GpioD.Value;
            gpioAval.Enabled = config.GpioA.Mode.Equals("INPUT") ? false : true;
            gpioBval.Enabled = config.GpioB.Mode.Equals("INPUT") ? false : true;
            gpioCval.Enabled = config.GpioC.Mode.Equals("INPUT") ? false : true;
            gpioDval.Enabled = config.GpioD.Mode.Equals("INPUT") ? false : true;
        }

        /// <summary>
        /// Move control values ​​to the configuration
        /// </summary>
        private void ControlsToConfig()
        {
            string NewGpioAmode = (string)gpioAmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
            string NewGpioBmode = (string)gpioBmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
            string NewGpioCmode = (string)gpioCmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
            string NewGpioDmode = (string)gpioDmode.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "OUTPUT" }, { 1, "INPUT" } });
            // Adjust config mode values if Controls have been change by User
            if (!config.GpioA.Mode.Equals(NewGpioAmode))
            {
                config.GpioA.Mode = NewGpioAmode;
                config.GpioA.change = true;
                config.Modify = true;
            }
            if (!config.GpioB.Mode.Equals(NewGpioBmode))
            {
                config.GpioB.Mode = NewGpioBmode;
                config.GpioB.change = true;
                config.Modify = true;
            }
            if (!config.GpioC.Mode.Equals(NewGpioCmode))
            {
                config.GpioC.Mode = NewGpioCmode;
                config.GpioC.change = true;
                config.Modify = true;
            }
            if (!config.GpioD.Mode.Equals(NewGpioDmode))
            {
                config.GpioD.Mode = NewGpioDmode;
                config.GpioD.change = true;
                config.Modify = true;
            }
            // Check the Config Modes and Reasign Values for OUTPUTS only
            if (config.GpioA.Mode.Equals("OUTPUT"))
            {
                if(config.GpioA.Value != gpioAval.Value)
                {
                    config.GpioA.Value = gpioAval.Value;
                    config.GpioA.change = true;
                    config.Modify = true;
                }
            }
            if (config.GpioB.Mode.Equals("OUTPUT"))
            {
                if (config.GpioB.Value != gpioBval.Value)
                {
                    config.GpioB.Value = gpioBval.Value;
                    config.GpioB.change = true;
                    config.Modify = true;
                }
            }
            if (config.GpioC.Mode.Equals("OUTPUT"))
            {
                if (config.GpioC.Value != gpioCval.Value)
                {
                    config.GpioC.Value = gpioCval.Value;
                    config.GpioC.change = true;
                    config.Modify = true;
                }
            }
            if (config.GpioD.Mode.Equals("OUTPUT"))
            {
                if (config.GpioD.Value != gpioDval.Value)
                {
                    config.GpioD.Value = gpioDval.Value;
                    config.GpioD.change = true;
                    config.Modify = true;
                }
            }
        }

        /// <summary>
        /// Display the form modally
        /// </summary>
        public static void ShowDialog(KPView.KPProperties kpProps, AppDirs appDirs, int kpNum)
        {
            if (kpProps == null)
                throw new ArgumentNullException("kpProps");

            gpioForm gpioConfig = new gpioForm
            {
                appDirs = appDirs,
                kpNum = kpNum
            };

            gpioConfig.ShowDialog();
        }

        private void gpioForm_Load(object sender, EventArgs e)
        {
            // Load configuration from file
            string errMsg;
            configFileName = gpioConfig.GetFileName(appDirs.ConfigDir, kpNum);
            if (File.Exists(configFileName) && !config.Load(configFileName, out errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }
            else
            {
                // Load the Form Controls
                ConfigToControls();

                gpioAmode.SelectedIndexChanged += new System.EventHandler(ModeSelect_SelectedIndexChanged);
                gpioBmode.SelectedIndexChanged += new System.EventHandler(ModeSelect_SelectedIndexChanged);
                gpioCmode.SelectedIndexChanged += new System.EventHandler(ModeSelect_SelectedIndexChanged);
                gpioDmode.SelectedIndexChanged += new System.EventHandler(ModeSelect_SelectedIndexChanged);

                // Set up a Timer
                Timer timer = new Timer
                {
                    Interval = (1 * 200) // 100 msec
                };
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string errMsg;
            config.Load(configFileName, out errMsg);
            ValueToControls();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // obtaining changes in configuration
            ControlsToConfig();

            // Save the XML file if any change in Form
            if (config.Modify == true)
            {
                // save the configuration
                string errMsg;
                if (config.Save(configFileName, out errMsg))
                    DialogResult = DialogResult.OK;
                else
                    ScadaUiUtils.ShowError(errMsg);
            }

            DialogResult = DialogResult.OK;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // obtaining changes in configuration
            ControlsToConfig();

            // Save the XML file if any change in Form
            if (config.Modify == true)
            {
                // save the configuration
                string errMsg;
                if (!config.Save(configFileName, out errMsg))
                    ScadaUiUtils.ShowError(errMsg);
            }
        }

    }
}
