using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scada.Comm.Channels;
using System.Xml;
using System.IO;

namespace Scada.Comm.Devices
{
    internal class SerConfig
    {

        public SerConfig()
        {
            SetToDefault();
        }

        private List<(int, string)> BaudRatesNum;
        private List<(int, string)> DataBitNum;
        private List<(int, string)> ParityNum;
        private List<(int, string)> StopNum;
        private List<(int, string)> FlowNum;

        public string Baud { get; set; }

        public string DataBit { get; set; }

        public string Parity { get; set; }

        public string Stop { get; set; }

        public string Flow { get; set; }

        public bool Modify { get; set; }

        private void SetToDefault()
        {
            Baud = "115200";
            DataBit = "8";
            Parity = "NONE";
            Stop = "1";
            Flow = "NONE";
            Modify = false;
            BaudRatesNum = new List<(int, string)>
            {
                (0, "300"),
                (1, "600"),
                (2, "1200"),
                (3, "1800"),
                (4, "2400"),
                (5, "4800"),
                (6, "9600"),
                (7, "14400"),
                (8, "19200"),
                (9, "28800"),
                (10, "38400"),
                (11, "57600"),
                (12, "115200"),
                (13, "230400")
            };
            DataBitNum = new List<(int, string)>
            {
                (0, "7"),
                (1, "8")
            };
            ParityNum = new List<(int, string)>
            {
                (0, "NONE"),
                (1, "ODD"),
                (2, "EVEN")
            };
            StopNum = new List<(int, string)>
            {
                (0, "1"),
                (1, "2")
            };
            FlowNum = new List<(int, string)>
            {
                (0, "NONE"),
                (1, "XON/XOFF"),
                (2, "RTS/CTS")
            };
        }
        
        // Get the Argument Value for Wiznet Commands

        public int GetBaudRate()
        {
            for(int i = 0; i < BaudRatesNum.Count; i++)
            {
                if (BaudRatesNum[i].Item2.Equals(Baud))
                    return (BaudRatesNum[i].Item1);
            }
            return 0;
        }
        public int GetDataBit()
        {
            for (int i = 0; i < DataBitNum.Count; i++)
            {
                if (DataBitNum[i].Item2.Equals(DataBit))
                    return (DataBitNum[i].Item1);
            }
            return 0;
        }
        public int GetParity()
        {
            for (int i = 0; i < ParityNum.Count; i++)
            {
                if (ParityNum[i].Item2.Equals(Parity))
                    return (ParityNum[i].Item1);
            }
            return 0;
        }
        public int GetStop()
        {
            for (int i = 0; i < StopNum.Count; i++)
            {
                if (StopNum[i].Item2.Equals(Stop))
                    return (StopNum[i].Item1);
            }
            return 0;
        }
        public int GetFlow()
        {
            for (int i = 0; i < FlowNum.Count; i++)
            {
                if (FlowNum[i].Item2.Equals(Flow))
                    return (FlowNum[i].Item1);
            }
            return 0;
        }

        public bool DecodeCommand(string cmd)
        {
            FileStream fileStream = new FileStream("C:\\serialDecodeLog.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.WriteLine("decode recv command: " + cmd);
            bool success = false;
            if (cmd.Contains("BR"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                sw.WriteLine("BR" + val.ToString());
                Baud = BaudRatesNum[val].Item2;
                success = true;
            }
            else if (cmd.Contains("DB"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                sw.WriteLine("DB" + val.ToString());
                DataBit = DataBitNum[val].Item2;
                success = true;
            }
            else if (cmd.Contains("PR"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                sw.WriteLine("PR" + val.ToString());
                Parity = ParityNum[val].Item2;
                success = true;
            }
            else if (cmd.Contains("SB"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                sw.WriteLine("SB" + val.ToString());
                Stop = StopNum[val].Item2;
                success = true;
            }
            else if (cmd.Contains("FL"))
            {
                int val = Int32.Parse(cmd.Substring(2));
                sw.WriteLine("FL" + val.ToString());
                Flow = FlowNum[val].Item2;
                success = true;
            }
            sw.Close();
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
                xmlDoc.Load(fileName);

                XmlElement rootElem = xmlDoc.DocumentElement;
                Baud = rootElem.GetChildAsString("Baud");
                DataBit = rootElem.GetChildAsString("DataBit");
                Parity = rootElem.GetChildAsString("Parity");
                Stop = rootElem.GetChildAsString("Stop");
                Flow = rootElem.GetChildAsString("Flow");
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

                XmlElement rootElem = xmlDoc.CreateElement("KpWiznetSerialConfig");
                xmlDoc.AppendChild(rootElem);

                rootElem.AppendElem("Baud", Baud);
                rootElem.AppendElem("DataBit", DataBit);
                rootElem.AppendElem("Parity", Parity);
                rootElem.AppendElem("Stop", Stop);
                rootElem.AppendElem("Flow", Flow);
                rootElem.AppendElem("Modify", Modify);

                xmlDoc.Save(fileName);
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
            return configDir + "KpWiznetSerial_" + CommUtils.AddZeros(kpNum, 3) + ".xml";
        }
    }
}
