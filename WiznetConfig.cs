/*
 * Copyright 2018 Manuel Iglesias
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : KpWiznet
 * Summary  : Device properties form
 * 
 * Author   : Manuel Iglesias
 * Created  : 2018
 * Modified : 2018
 */

using Scada.UI;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;


namespace Scada.Comm.Devices.KpWiznet
{

    /// <summary>
    /// Device properties form
    /// <para>Form for setting properties of KP</para>
    /// </summary>
    public partial class WiznetConfig : Form
    {
        private AppDirs appDirs;       // the application directory
        private int kpNum;             // number of customizable control unit
        private Config config;         // KP configuration
        private string configFileName; // configuration file name
        private KPView.KPProperties kpProps;
        private UDPer udp = null;
        private CMD_Parser cmd_parser;
        private bool pending = false;

        /// <summary>
        /// Constructor that restricts the creation of a form without parameters
        /// </summary>
        private WiznetConfig()
        {
            InitializeComponent();

            appDirs = null;
            kpNum = 0;
            config = new Config();
            configFileName = "";
            cmd_parser = new CMD_Parser();
            if (udp == null) {
                udp = new UDPer();
                udp.AssignRxCb(UdpRxMethod);
                udp.Start();
            }
        }

        ~WiznetConfig()  // destructor
        {
            udp.Stop();
            // cleanup statements...
        }

        /// <summary>
        /// Set the controls according to the configuration
        /// </summary>
        private void ConfigToControls()
        {
            numPort.SetValue(config.Port);
            txtIPaddr.Text = config.IPaddr;
            txtMask.Text = config.Mask;
            txtGateway.Text = config.Gateway;
            radioDHCP.Checked = config.DHCP;
            enPolling.Checked = config.PollingCfg;
        }


        /// <summary>
        /// Move control values ​​to the configuration
        /// </summary>
        private void ControlsToConfig()
        {
            config.Port = Convert.ToInt32(numPort.Value);
            config.IPaddr = txtIPaddr.Text;
            config.Mask = txtMask.Text;
            config.Gateway = txtGateway.Text;
            config.DHCP = radioDHCP.Checked;
            config.PollingCfg = enPolling.Checked;
        }


        /// <summary>
        /// Display the form modally
        /// </summary>
        public static void ShowDialog(KPView.KPProperties kpProps, AppDirs appDirs, int kpNum)
        {
            if (appDirs == null)
                throw new ArgumentNullException("appDirs");

            WiznetConfig frmConfig = new WiznetConfig
            {
                appDirs = appDirs,
                kpNum = kpNum,
                kpProps = kpProps
            };


            frmConfig.ShowDialog();
        }

        // Create a method for Rx Callback
        public bool UdpRxMethod(byte[] data)
        {
            if (cmd_parser != null && pending == true)
            {
                cmd_parser.CmdProcess(data);
                devicesMAC.Items.Clear();
                devicesMAC.Items.Add(cmd_parser.Mac_string);
                txtIPaddr.Text = cmd_parser.Li_string;
                txtMask.Text = cmd_parser.Sm_string;
                txtGateway.Text = cmd_parser.Gw_string;
                int ip_alloc = 0;
                if (Int32.TryParse(cmd_parser.Im_string, out ip_alloc))
                {
                    if (ip_alloc == 0)
                    {
                        radioDHCP.Checked = false;
                        radioStatic.Checked = true;
                        txtIPaddr.Enabled = true;
                        txtMask.Enabled = true;
                        txtGateway.Enabled = true;
                    }
                    else
                    {
                        radioDHCP.Checked = true;
                        radioStatic.Checked = false;
                        txtIPaddr.Enabled = false;
                        txtMask.Enabled = false;
                        txtGateway.Enabled = false;
                    }
                }
                textPName.Text = cmd_parser.Mn_string;
                textFirmware.Text = cmd_parser.Vr_string;
                pending = false;
                return true;
            }
            else
            {
                if (cmd_parser == null)
                    return false;
                else
                    return true;     // this will discard any data
            }
            // this was just for debugging
            //string message = BitConverter.ToString(data);
            //FileStream fileStream = new FileStream("C:\\discoLog.txt", FileMode.Append);
            //StreamWriter sw = new StreamWriter(fileStream);
            //sw.WriteLine("UDP Receive: " + message + "\r\n");
            //sw.Close();
        }

        private void WiznetConfig_Load(object sender, EventArgs e)
        {
            // module localization
            string errMsg;
            if (!Localization.UseRussian)
            {
                if (Localization.LoadDictionaries(appDirs.LangDir, "KpTest", out errMsg))
                    Translator.TranslateForm(this, "Scada.Comm.Devices.KpTest.WiznetConfig");
                else
                    ScadaUiUtils.ShowError(errMsg);
            }

            // header output
            Text = string.Format(Text, kpNum);

            // loading configuration
            configFileName = Config.GetFileName(appDirs.ConfigDir, kpNum);
            if (File.Exists(configFileName) && !config.Load(configFileName, out errMsg))
                ScadaUiUtils.ShowError(errMsg);

            // output configuration CP
            ConfigToControls();

            // disable the PORT Text Field - Always 50001 for Now to simplify things
            numPort.Enabled = false;
        }

        private void btnEditSerialSettings_Click(object sender, EventArgs e)
        {
            // Edit Serial Settings Skhow Dialog
            SerForm.ShowDialog(kpProps, appDirs, kpNum);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // obtaining changes in configuration
            ControlsToConfig();

            // save the configuration
            string errMsg;

            if (config.Save(configFileName, out errMsg))
                DialogResult = DialogResult.OK;
            else
                ScadaUiUtils.ShowError(errMsg);

            udp.Stop();
            Dispose();
            Close();
        }

        private void bttnGPIO_Click(object sender, EventArgs e)
        {
            gpioForm.ShowDialog(kpProps, appDirs, kpNum);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string errMsg;
            ControlsToConfig();
            if (!config.Save(configFileName, out errMsg))
                ScadaUiUtils.ShowError(errMsg);

        }

        private void btnDisco_Click(object sender, EventArgs e)
        {
            byte[] disco = { 0x4d, 0x41, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x0d, 0x0a,       // MA <BROADCAST MAC> 
                            0x50, 0x57, 0x20, 0x0d, 0x0a,  // PW <0x20>
                            0x4d, 0x43, 0x0d, 0x0a,        // MC    MAC address
                            0x56, 0x52, 0x0d, 0x0a,        // VR    Firmware version
                            0x4d, 0x4e, 0x0d, 0x0a,        // MN    Product name
                            0x53, 0x54, 0x0d, 0x0a,        // ST    Operation status
                            0x49, 0x4d, 0x0d, 0x0a,        // IM    IP address allocation method 
                            0x4f, 0x50, 0x0d, 0x0a,        // OP    Network operation mode
                            0x4c, 0x49, 0x0d, 0x0a,        // LI    Local IP address
                            0x53, 0x4d, 0x0d, 0x0a,        // SM    Subnet mask
                            0x47, 0x57, 0x0d, 0x0a        // GW    Gateway address
                            };
            pending = true;
            udp.Send(disco, (int)numPort.Value);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            udp.Stop();
            Dispose();
            Close();
        }

        private void radioStatic_CheckedChanged(object sender, EventArgs e)
        {
            radioDHCP.Checked = radioStatic.Checked ? false : true;
            if (radioDHCP.Checked)
            {
                txtIPaddr.Enabled = false;
                txtMask.Enabled = false;
                txtGateway.Enabled = false;
            }
        }

        private void radioDHCP_CheckedChanged(object sender, EventArgs e)
        {
            radioStatic.Checked = radioDHCP.Checked ? false : true;
            if (radioStatic.Checked)
            {
                txtIPaddr.Enabled = true;
                txtMask.Enabled = true;
                txtGateway.Enabled = true;
            }
        }

        private void BtnGwMode_Click(object sender, EventArgs e)
        {
            byte[] disco = { 0x4d, 0x41, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x0d, 0x0a,        // MA <BROADCAST MAC> 
                            0x50, 0x57, 0x20, 0x0d, 0x0a,                                       // PW <0x20>
                            0x45, 0x58, 0x0d, 0x0a                                             // EX command
            };
            pending = false;        // we don't need command processing here
            udp.Send(disco, (int)numPort.Value);
        }
    }

    class UDPer
    {
        public delegate bool RxCb(byte[] data);

        RxCb ExternalRxCb = null;
        const int PORT_NUMBER = 5000;

        public void Start()
        {
            // StartListening();        // I do not do tthis since it start an async reception that later hangs if stoped
        }
        public void Stop()
        {
            try
            {
                udp.Close();
            }
            catch
            {
                // don't care
            }
        }

        public void AssignRxCb(RxCb rx)
        {
            ExternalRxCb += rx;
        }

        private readonly UdpClient udp = new UdpClient(PORT_NUMBER);
        IAsyncResult ar_ = null;

        private void StartListening()
        {
            ar_ = udp.BeginReceive(Receive, new object());
        }
        private void Receive(IAsyncResult ar)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
            byte[] bytes = udp.EndReceive(ar, ref ip);

            if(ExternalRxCb != null)
            {
                if (!ExternalRxCb(bytes))
                {
                    string errMsg = "UDP reception Cb error";
                    ScadaUiUtils.ShowError(errMsg);
                }
            }
            //StartListening();
        }
        public void Send(byte[] bytes, int dstPort=50001)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("255.255.255.255"), dstPort);
            udp.Send(bytes, bytes.Length, ip);
            StartListening();
        }

    }
 
    class CMD_Parser
    {
        public string Mac_string { get; set; }   // Mac Address string
        public string Mn_string { get; set; }    // Product name string
        public string Vr_string { get; set; }    // Product version string
        public string Mode_string { get; set; }  // Operation mode string
        public string Li_string { get; set; }    // Local IP string
        public string Im_string { get; set; }    // Ip allocation method
        public string St_string { get; set; }    // Operation Status
        public string Sm_string { get; set; }    // Subnet Mask Address
        public string Gw_string { get; set; }    // Gateway Adddress

        private string[] cmd_lines;

        /// <summary>
        /// Set default configuration parameter values
        /// </summary>
        private void SetToDefault()
        {
            Mac_string = System.String.Empty;
            Mn_string = System.String.Empty;
            Vr_string = System.String.Empty;
            Mode_string = System.String.Empty;
            Li_string = System.String.Empty;
            Im_string = System.String.Empty;
            St_string = System.String.Empty;
            Sm_string = System.String.Empty;
            Gw_string = System.String.Empty;
        }

        public CMD_Parser()
        {
            SetToDefault();
        }

        public void CmdProcess(byte[] data)
        {
            string message = Encoding.ASCII.GetString(data);
            cmd_lines = message.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            );
            for (int j = 0; j< cmd_lines.Length; j++)
            {
                if (cmd_lines[j].Contains("MC"))
                {
                    Mac_string = cmd_lines[j].Substring(2);
                }
                else if (cmd_lines[j].Contains("MN"))
                {
                    Mn_string = cmd_lines[j].Substring(2);
                }
                else if (cmd_lines[j].Contains("VR"))
                {
                    Vr_string = cmd_lines[j].Substring(2);
                }
                else if (cmd_lines[j].Contains("OP"))
                {
                    Mode_string = cmd_lines[j].Substring(2);
                }
                else if (cmd_lines[j].Contains("LI"))
                {
                    Li_string = cmd_lines[j].Substring(2);
                }
                else if (cmd_lines[j].Contains("IM"))
                {
                    Im_string = cmd_lines[j].Substring(2);
                }
                else if (cmd_lines[j].Contains("ST"))
                {
                    St_string = cmd_lines[j].Substring(2);
                }
                else if (cmd_lines[j].Contains("SM"))
                {
                    Sm_string = cmd_lines[j].Substring(2);
                }
                else if (cmd_lines[j].Contains("GW"))
                {
                    Gw_string = cmd_lines[j].Substring(2);
                }
            }
        }
    }
}
