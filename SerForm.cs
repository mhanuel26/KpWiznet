using Scada.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Scada.Comm.Devices.KpWiznet
{
    public partial class SerForm : Form
    {
        //private KPView.KPProperties kpProps;
        private SerConfig config;
        private string configFileName; // configuration file name
        private AppDirs appDirs;       // the application directory
        private int kpNum;             // number of customizable control unit

        public SerForm()
        {
            InitializeComponent();

            config = new SerConfig();
        }

        /// <summary>
        /// Set the controls according to the configuration
        /// </summary>
        private void ConfigToControls()
        {
            //string Bauds = kpProps.CustomParams.GetStringParam("Bauds", false, config.Baud);
            serBauds.SetSelectedItem(config.Baud, new Dictionary<string, int>()
            {
                { "115200", 0 }, { "57600", 1 }, { "38400", 2 }, { "28800", 3 }, { "19200", 4 },
                { "14400", 5 }, { "9600", 6 }, { "4800", 7 }, { "2400", 8 }, { "1800", 9 },
                { "1200", 10 }, { "600", 11 },{ "300", 12 }
            }, 0);
            serDataBit.SetSelectedItem(config.DataBit, new Dictionary<string, int>() { { "7", 0 }, { "8", 1 } }, 0);
            serParity.SetSelectedItem(config.Parity, new Dictionary<string, int> { { "NONE", 0 }, { "ODD", 1 }, { "EVEN", 2 } }, 0);
            serStop.SetSelectedItem(config.Stop, new Dictionary<string, int>() { { "1", 0 }, { "2", 1 } }, 0);
            serFlow.SetSelectedItem(config.Flow, new Dictionary<string, int> { {"NONE", 0 }, { "XON/XOFF", 1 }, { "RTS/CTS", 2 }, { "RTS on TX", 3 }, { "RTS on TX (invert)", 4} }, 0);
        }

        /// <summary>
        /// Move control values ​​to the configuration
        /// </summary>
        private void ControlsToConfig()
        {

            // Get the Form Values
            string NewBaud = (string)serBauds.GetSelectedItem(
                new Dictionary<int, object>() { { 0, "115200" }, { 1, "57600" }, { 2, "38400"}, { 3, "28800" },
                    { 4, "19200" }, { 5, "14400" }, { 6, "9600" }, { 7, "4800"}, { 8, "2400" }, { 9, "1800" },
                    { 10, "1200" }, { 11, "600" }, { 12, "300" } });
            string NewDataBit = (string)serDataBit.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "7" }, { 1, "8" } });
            string NewParity = (string)serParity.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "NONE" }, { 1, "ODD" }, { 2, "EVEN" } });
            string NewStop = (string)serStop.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "1" }, { 1, "2" } });
            string NewFlow = (string)serFlow.GetSelectedItem(
                    new Dictionary<int, object>() { { 0, "NONE" }, { 1, "XON/XOFF" }, { 2, "RTS/CTS" }, { 3, "RTS on TX" }, { 4, "RTS on TX (invert)" } });

            // Check wether or not any field have change
            if ((config.Baud != NewBaud) || (config.DataBit != NewDataBit) || (config.Parity != NewParity) || (config.Stop != NewStop) || (config.Flow != NewFlow))
            {
                config.Baud = NewBaud;
                config.DataBit = NewDataBit;
                config.Parity = NewParity;
                config.Stop = NewStop;
                config.Flow = NewFlow;
                config.Modify = true;
            }

        }

        /// <summary>
        /// Display the form modally
        /// </summary>
        public static void ShowDialog(KPView.KPProperties kpProps, AppDirs appDirs, int kpNum)
        {
            if (kpProps == null)
                throw new ArgumentNullException("kpProps");

            SerForm serConfig = new SerForm
            {
                //kpProps = kpProps,
                appDirs = appDirs,
                kpNum = kpNum
            };

            serConfig.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // obtaining changes in configuration
            ControlsToConfig();

            // Save the XML file if any change in Form
            if(config.Modify == true)
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

        private void SerForm_Load(object sender, EventArgs e)
        {
            // Load configuration from File
            string errMsg;
            configFileName = SerConfig.GetFileName(appDirs.ConfigDir, kpNum);
            if (File.Exists(configFileName) && !config.Load(configFileName, out errMsg))
                ScadaUiUtils.ShowError(errMsg);

            ConfigToControls();
        }

    }
}
