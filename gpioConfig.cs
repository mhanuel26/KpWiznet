using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Scada.Comm.Devices
{
    class GpioClass
    {
        public bool change;
        public string Mode;
        public decimal Value;

        public GpioClass(string mode, int value)
        {
            this.Mode = mode;
            this.Value = value;
            this.change = false;
        }
    }

    class gpioConfig
    {
        public gpioConfig()
        {
            SetToDefault();
        }

        static readonly object _fileAccess = new object();
        private List<(int, string)> GpioMode;
        public GpioClass GpioA;
        public GpioClass GpioB;
        public GpioClass GpioC;
        public GpioClass GpioD;

        public bool Modify { get; set; }

        private void SetToDefault()
        {
            GpioMode = new List<(int, string)>
            {
                (0, "INPUT"),
                (1, "OUTPUT")
            };
            GpioA = new GpioClass("INPUT", 0);
            GpioB = new GpioClass("INPUT", 0);
            GpioC = new GpioClass("INPUT", 0);
            GpioD = new GpioClass("INPUT", 0);
            Modify = false;
        }

        /// <summary>
        /// Get GPIO Mode
        /// </summary>
        public int GetGpioMode(GpioClass gpio)
        {
            for (int i = 0; i < GpioMode.Count; i++)
            {
                if (GpioMode[i].Item2.Equals(gpio.Mode))
                    return (GpioMode[i].Item1);
            }
            return 0;
        }

        /// <summary>
        /// Get GPIO Mode
        /// </summary>
        public string GetGpioModeString(GpioClass gpio)
        {
            string mode = System.String.Empty;
            for (int i = 0; i < GpioMode.Count; i++)
            {
                if (GpioMode[i].Item2.Equals(gpio.Mode))
                {
                    mode = GpioMode[i].Item2;
                    break;
                }
            }
            return mode;
        }

        /// <summary>
        /// Get GPIO Value
        /// </summary>
        public int GetGpioValue(GpioClass gpio)
        {
            return (int)gpio.Value;
        }


        /// <summary>
        /// Set GPIO Mode
        /// </summary>
        private void SetGpioMode(GpioClass gpio, int mode)
        {
            gpio.Mode = GpioMode[mode].Item2;
        }

        /// <summary>
        /// Set GPIO Value
        /// </summary>
        public void SetGpioValue(GpioClass gpio, int value)
        {
            gpio.Value = value;
        }

        public bool DecodeCommand(string cmd)
        {
            //FileStream fileStream = new FileStream("C:\\gpioDecodeLog.txt", FileMode.Append);
            //StreamWriter sw = new StreamWriter(fileStream);
            //sw.WriteLine("decode command: " + cmd);
            bool success = false;

            if (cmd.Contains("CA"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                //sw.WriteLine("CA" + val.ToString());
                SetGpioMode(GpioA, val);
                success = true;
            }
            else if (cmd.Contains("CB"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                //sw.WriteLine("CB" + val.ToString());
                SetGpioMode(GpioB, val);
                success = true;
            }
            else if (cmd.Contains("CC"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                //sw.WriteLine("CC" + val.ToString());
                SetGpioMode(GpioC, val);
                success = true;
            }
            else if (cmd.Contains("CD"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                //sw.WriteLine("CD" + val.ToString());
                SetGpioMode(GpioD, val);
                success = true;
            }
            else if (cmd.Contains("GA"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                //sw.WriteLine("GA" + val.ToString());
                SetGpioValue(GpioA, val);
                success = true;
            }
            else if (cmd.Contains("GB"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                //sw.WriteLine("GB" + val.ToString());
                SetGpioValue(GpioB, val);
                success = true;
            }
            else if (cmd.Contains("GC"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                //sw.WriteLine("GC" + val.ToString());
                SetGpioValue(GpioC, val);
                success = true;
            }
            else if (cmd.Contains("GD"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                //sw.WriteLine("GD" + val.ToString());
                SetGpioValue(GpioD, val);
                success = true;
            }
            //sw.Close();
            return success;
        }

            /// <summary>
            /// Download configuration from file
            /// </summary>

            public bool Load(string fileName, out string errMsg)
        {
            SetToDefault();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                lock (_fileAccess)
                {
                    xmlDoc.Load(fileName);
                }

                XmlElement rootElem = xmlDoc.DocumentElement;
                GpioA.Mode = rootElem.GetChildAsString("GpioAmode");
                GpioB.Mode = rootElem.GetChildAsString("GpioBmode");
                GpioC.Mode = rootElem.GetChildAsString("GpioCmode");
                GpioD.Mode = rootElem.GetChildAsString("GpioDmode");

                GpioA.Value = rootElem.GetChildAsInt("GpioAvalue");
                GpioB.Value = rootElem.GetChildAsInt("GpioBvalue");
                GpioC.Value = rootElem.GetChildAsInt("GpioCvalue");
                GpioD.Value = rootElem.GetChildAsInt("GpioDvalue");

                GpioA.change = rootElem.GetChildAsBool("GpioAchange");
                GpioB.change = rootElem.GetChildAsBool("GpioBchange");
                GpioC.change = rootElem.GetChildAsBool("GpioCchange");
                GpioD.change = rootElem.GetChildAsBool("GpioDchange");

                Modify = rootElem.GetChildAsBool("Modify");

                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.LoadKpSettingsError + ":" + Environment.NewLine + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Save configuration to file
        /// </summary>
        public bool Save(string fileName, out string errMsg)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDecl);

                XmlElement rootElem = xmlDoc.CreateElement("KpWiznetGpioConfig");
                xmlDoc.AppendChild(rootElem);

                rootElem.AppendElem("GpioAmode", GpioA.Mode);
                rootElem.AppendElem("GpioBmode", GpioB.Mode);
                rootElem.AppendElem("GpioCmode", GpioC.Mode);
                rootElem.AppendElem("GpioDmode", GpioD.Mode);

                rootElem.AppendElem("GpioAvalue", GpioA.Value);
                rootElem.AppendElem("GpioBvalue", GpioB.Value);
                rootElem.AppendElem("GpioCvalue", GpioC.Value);
                rootElem.AppendElem("GpioDvalue", GpioD.Value);

                rootElem.AppendElem("GpioAchange", GpioA.change);
                rootElem.AppendElem("GpioBchange", GpioB.change);
                rootElem.AppendElem("GpioCchange", GpioC.change);
                rootElem.AppendElem("GpioDchange", GpioD.change);

                rootElem.AppendElem("Modify", Modify);

                lock (_fileAccess)
                {
                    xmlDoc.Save(fileName);
                }
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.SaveKpSettingsError + ":" + Environment.NewLine + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Get the name of the configuration file
        /// </summary>
        public static string GetFileName(string configDir, int kpNum)
        {
            return configDir + "KpWiznetGpio_" + CommUtils.AddZeros(kpNum, 3) + ".xml";
        }

    }
}
