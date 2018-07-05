/*
 * Copyright 2015 Mikhail Shiryaev
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
 * Module   : KpSms
 * Summary  : Device communication logic
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2006
 * Modified : 2015
 * 
 * Description
 * Device library for testing.
 */

using Scada.Comm.Channels;
using Scada.Comm.Devices.KpWiznet;
using Scada.Data.Configuration;
using Scada.Data.Models;
using Scada.Data.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scada.Comm.Devices
{
    /// <summary>
    /// Device communication logic
    /// <para>Логика работы КП</para>
    /// </summary>
    public class KpWiznetLogic : KPLogic
    {
        private Config config;
        private SerConfig wiznetSerCfg;
        private gpioConfig wiznetGpioCfg;
        private bool fatalError;                    // fatal error when initializing KP
        private string state;                       // state of KP
        private static readonly Connection.TextStopCondition ReadStopCondition = 
            new Connection.TextStopCondition("OK");
        private bool starting = false;

        private Random random;
        private DateTime lastGpioRead;
        private const int GPIO_REFRESH = 100;      // expressed in milliseconds

        //private static Connection baseConnection;

        public KpWiznetLogic(int number)
            : base(number)
        {
            config = new Config();
            wiznetSerCfg = new SerConfig();
            wiznetGpioCfg = new gpioConfig();
            random = new Random();
            CanSendCmd = true;

            List<TagGroup> tagGroups = new List<TagGroup>();
            TagGroup tagGroup = new TagGroup("Group 1");
            tagGroup.KPTags.Add(new KPTag(1, "Tag 1"));
            tagGroup.KPTags.Add(new KPTag(2, "Tag 2"));
            tagGroup.KPTags.Add(new KPTag(3, "Tag 3"));
            tagGroup.KPTags.Add(new KPTag(4, "Tag 4"));
            tagGroup.KPTags.Add(new KPTag(5, "Tag 5"));
            tagGroups.Add(tagGroup);

            tagGroup = new TagGroup("Group 2");
            tagGroup.KPTags.Add(new KPTag(6, "Tag 6"));
            tagGroup.KPTags.Add(new KPTag(7, "Tag 7"));
            tagGroup.KPTags.Add(new KPTag(8, "Tag 8"));
            tagGroup.KPTags.Add(new KPTag(9, "Tag 9"));
            tagGroup.KPTags.Add(new KPTag(10, "Tag 10"));
            tagGroups.Add(tagGroup);

            InitKPTags(tagGroups);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] StringToByteArray(String hex)
        {
            Console.WriteLine("StringToByteArray: " + hex);
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }


        /// <summary>
        /// Download the configuration of the connection to the Wiznet Scada Device
        /// </summary>
        private void LoadConfig()
        {
            string errMsg;
            fatalError = !config.Load(Config.GetFileName(AppDirs.ConfigDir, Number), out errMsg);

            if (fatalError)
            {
                state = "Sending notifocations is impossible";
                throw new Exception(errMsg);
            }
            else
            {
                state = "Waiting for commands...";
            }
        }

        private void LoadSerialConfig()
        {
            string errMsg;
            fatalError = !wiznetSerCfg.Load(SerConfig.GetFileName(AppDirs.ConfigDir, Number), out errMsg);

            if (fatalError)
            {
                state = "Load Serial Settings not possible";
                throw new Exception(errMsg);
            }
            else
            {
                state = "Serial Settings Loaded";
            }

        }
        private void SaveSerialConfig()
        {
            string errMsg;
            fatalError = !wiznetSerCfg.Save(SerConfig.GetFileName(AppDirs.ConfigDir, Number), out errMsg);

            if (fatalError)
            {
                state = "Save Serial Settings not possible.";
                throw new Exception(errMsg);
            }
            else
            {
                state = "Serial Settings Saved to File.";
            }
        }

        private void LoadGpioConfig()
        {
            string errMsg;
            fatalError = !wiznetGpioCfg.Load(gpioConfig.GetFileName(AppDirs.ConfigDir, Number), out errMsg);

            if (fatalError)
            {
                state = "Load GPIO Settings not possible";
                throw new Exception(errMsg);
            }
            else
            {
                state = "GPIO Seetings Loaded";
            }

        }
        private void SaveGpioConfig()
        {
            string errMsg;
            fatalError = !wiznetGpioCfg.Save(gpioConfig.GetFileName(AppDirs.ConfigDir, Number), out errMsg);

            if (fatalError)
            {
                state = "Save Serial Settings not possible.";
                throw new Exception(errMsg);
            }
            else
            {
                state = "GPIO Settings Saved to File.";
            }
        }

        //public static bool SendCommandToWiznet()
        //{
        //    if (baseConnection == null || !baseConnection.Connected)
        //    {
        //        FileStream fileStream = new FileStream("C:\\miLog.txt", FileMode.Append);
        //        StreamWriter sw = new StreamWriter(fileStream);
        //        sw.WriteLine("baseConnection NULL or Not Connected");
        //        sw.Close();
        //        return false;
        //    }
        //    else
        //    {
        //        baseConnection.WriteLine("Test");
        //        return true;
        //    }
        //}

        public void DiscardConnectionData()
        {
            // Discard anything that in pending in the Last Connection Command
            // Otherwise next usage of Connection probably will fail since will 
            // encounter garbage there
            string tmp;
            while (true)
            {
                tmp = Connection.ReadLine(100, out string logText);
                if (tmp == null)
                    break;
            }
        }

        public bool GetSerDeviceCfg()
        {
            // this is just for debugging
            FileStream fileStream = new FileStream("C:\\serialCmdLog.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.WriteLine("GetSerDeviceCfg");

            string logText;
            string cmd = "BR\r\nDB\r\nPR\r\nSB\r\nFL\r\n";

            // Build Network Command
            byte[] cmd_hex = { 0x4d, 0x41, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x0d, 0x0a, 0x50, 0x57, 0x20, 0x0d, 0x0a };
            byte[] bytes = Encoding.ASCII.GetBytes(cmd);
            int final_len = cmd_hex.Length + bytes.Length;
            byte[] final_cmd = new byte[final_len + 1];
            Array.ConstrainedCopy(cmd_hex, 0, final_cmd, 0, cmd_hex.Length);
            Array.ConstrainedCopy(bytes, 0, final_cmd, cmd_hex.Length, bytes.Length);
            // Send the command
            sw.WriteLine("UDP Sent Command");
            Connection.Write(final_cmd, 0, final_len, CommUtils.ProtocolLogFormats.Hex, out logText);
            sw.WriteLine("logText:" + logText);
            byte[] par = new byte[100];
            int partial = Connection.Read(par, 0, 15, 1000);
            if (partial == 0)
            {
                // Timeout
                sw.WriteLine("UDP Receive Timeout\r\n");
                sw.Close();
                return false;
            }

            string tmp = System.String.Empty;
            while (true)
            {
                tmp = Connection.ReadLine(100, out logText);
                if (tmp == null)
                {
                    // Timeout
                    break;
                }
                else
                {
                    sw.WriteLine("Receive Line: " + tmp);
                    try
                    {
                        if (!wiznetSerCfg.DecodeCommand(tmp.Trim()))
                        {
                            DiscardConnectionData();
                            sw.Close();
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        DiscardConnectionData();
                        sw.WriteLine("{0} Exception caught.", e);
                        sw.Close();
                        return false;
                    }
                    continue;
                }
            }
            sw.WriteLine("Decode OK");
            sw.Close();
            return true;
        }

        public bool GetInputGpioValues()
        {

            // this is just for debugging
            //FileStream fileStream = new FileStream("C:\\GpioIntervalLog.txt", FileMode.Append);
            //StreamWriter sw = new StreamWriter(fileStream);

            bool success = false;
            StringBuilder command = new StringBuilder("");
            //build the command to read INPUTS
            if (wiznetGpioCfg.GetGpioModeString(wiznetGpioCfg.GpioA).Equals("INPUT"))
            {
                command.Append("GA\r\n");
            }
            if (wiznetGpioCfg.GetGpioModeString(wiznetGpioCfg.GpioB).Equals("INPUT"))
            {
                command.Append("GB\r\n");
            }
            if (wiznetGpioCfg.GetGpioModeString(wiznetGpioCfg.GpioC).Equals("INPUT"))
            {
                command.Append("GC\r\n");
            }
            if (wiznetGpioCfg.GetGpioModeString(wiznetGpioCfg.GpioD).Equals("INPUT"))
            {
                command.Append("GD\r\n");
            }
            if (command.ToString().Equals(System.String.Empty))
            {
                //sw.WriteLine("GetInputGpioValues NO COMMAND - ALL OUTPUTS?");
                //sw.Close();
                return false;
            }
            // build the complete network command
            string logText;
            byte[] cmd_hex = { 0x4d, 0x41, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x0d, 0x0a, 0x50, 0x57, 0x20, 0x0d, 0x0a };
            byte[] cmdBytes = Encoding.ASCII.GetBytes(command.ToString());
            int final_len = cmd_hex.Length + cmdBytes.Length;
            // reserve the final command buffer
            byte[] final_cmd = new byte[final_len + 1];
            // combine the network header and command
            Array.ConstrainedCopy(cmd_hex, 0, final_cmd, 0, cmd_hex.Length);
            Array.ConstrainedCopy(cmdBytes, 0, final_cmd, cmd_hex.Length, cmdBytes.Length);
            // Send the command
            Connection.Write(final_cmd, 0, final_len, CommUtils.ProtocolLogFormats.Hex, out logText);
            byte[] par = new byte[100];
            int partial = Connection.Read(par, 0, 15, 1000);
            if (partial == 0)
            {
                // Timeout
                //sw.WriteLine("GetInputGpioValues Timeout");
                //sw.Close();
                return false;
            }

            string tmp = System.String.Empty;
            while (true)
            {
                tmp = Connection.ReadLine(100, out logText);
                if (tmp == null)
                {
                    // Timeout
                    break;
                }
                else
                {
                    try
                    {
                        if (!wiznetGpioCfg.DecodeCommand(tmp.Trim()))
                        {
                            DiscardConnectionData();
                            //sw.WriteLine("GetInputGpioValues Fail decode");
                            //sw.Close();
                            return false;
                        }
                        else
                        {
                            success = true;
                        }
                    }
                    catch //(Exception e)
                    {
                        //sw.WriteLine("GetInputGpioValues Exception: " + e);
                        //sw.Close();
                        DiscardConnectionData();
                        return false;
                    }
                    continue;
                }
            }
            return success;
        }

        public bool GetDeviceGpioCfg()
        {
            // this is just for debugging
            FileStream fileStream = new FileStream("C:\\gpioCmdLog.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.WriteLine("GetDeviceGpioCfg");

            string logText;
            // BaudRate Query Command
            string command = "CA\r\nCB\r\nCC\r\nCD\r\nGA\r\nGB\r\nGC\r\nGD\r\n";
            byte[] cmd_hex = { 0x4d, 0x41, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x0d, 0x0a, 0x50, 0x57, 0x20, 0x0d, 0x0a };
            byte[] cmdBytes = Encoding.ASCII.GetBytes(command);
            int final_len = cmd_hex.Length + cmdBytes.Length;
            // reserve the final command buffer
            byte[] final_cmd = new byte[final_len + 1];
            // combine the network header and command
            Array.ConstrainedCopy(cmd_hex, 0, final_cmd, 0, cmd_hex.Length);
            Array.ConstrainedCopy(cmdBytes, 0, final_cmd, cmd_hex.Length, cmdBytes.Length);
            // Send the command
            sw.WriteLine("UDP Sent Command");
            Connection.Write(final_cmd, 0, final_len, CommUtils.ProtocolLogFormats.Hex, out logText);
            sw.WriteLine("logText:" + logText);
            byte[] par = new byte[100];
            int partial = Connection.Read(par, 0, 15, 1000);
            if (partial == 0)
            {
                // Timeout
                sw.WriteLine("UDP Receive Timeout\r\n");
                sw.Close();
                return false;
            }

            string tmp = System.String.Empty;
            while (true)
            {
                tmp = Connection.ReadLine(100, out logText);
                if (tmp == null)
                {
                    // Timeout
                    break;
                }
                else
                {
                    sw.WriteLine("Receive Line: " + tmp);
                    try
                    {
                        if (!wiznetGpioCfg.DecodeCommand(tmp.Trim()))
                        {
                            DiscardConnectionData();
                            sw.Close();
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        DiscardConnectionData();
                        sw.WriteLine("{0} Exception caught.", e);
                        sw.Close();
                        return false;
                    }
                    continue;
                }
            }
            lastGpioRead = DateTime.Now;
            sw.WriteLine("Decode OK");
            sw.Close();
            return true;
        }

        public override void Session()
        {
            base.Session();

            if (starting == true)
            {
                starting = false;
                if (GetSerDeviceCfg())
                {
                    wiznetSerCfg.Modify = false;
                    SaveSerialConfig();
                }
                if (GetDeviceGpioCfg())
                {
                    wiznetGpioCfg.Modify = false;
                    SaveGpioConfig();
                }
            }
            // Load the configuration to reflect the changes
            LoadConfig();
            if (fatalError)
            {
                WriteToLog(state);
            }
            else
            {
                if (config.PollingCfg == true)
                {
                    try
                    {
                        TimeSpan interval = DateTime.Now - lastGpioRead;
                        if (interval.TotalMilliseconds >= GPIO_REFRESH)
                        {
                            lastGpioRead = DateTime.Now;
                            if (GetInputGpioValues())
                            {
                                SaveGpioConfig();
                            }
                        }
                    }
                    catch //(Exception e)
                    {
                        // take action here
                    }
                }
            }

            LoadSerialConfig();
            if (fatalError)
            {
                WriteToLog(state);
            }
            else
            {
                if (wiznetSerCfg.Modify == true)
                {
                    // parse the serial command
                    // BaudRates Set Command
                    string BrCmd = "BR" + wiznetSerCfg.GetBaudRate() + "\r\n";
                    // Data Bit Set Command
                    string DbCmd = "DB" + wiznetSerCfg.GetDataBit() + "\r\n";
                    // Parity Set Command
                    string ParCmd = "PR" + wiznetSerCfg.GetParity() + "\r\n";
                    // Stop Bit Set Command
                    string SbCmd = "SB" + wiznetSerCfg.GetStop() + "\r\n";
                    // Flow Set Command
                    string FlCmd = "FL" + wiznetSerCfg.GetFlow() + "\r\n";

                    // Build Network Command
                    byte[] cmd_hex = new byte[100]; // not sure how much will use
                    byte[] tmp_cmd;
                    byte[] end_cmd = { 0x0d, 0x0a };
                    int i = 0;

                    // Required for Network Communications
                    string command = "MA";
                    tmp_cmd = Encoding.ASCII.GetBytes(command);
                    Array.ConstrainedCopy(tmp_cmd, 0, cmd_hex, i, tmp_cmd.Length);
                    i += tmp_cmd.Length;
                    tmp_cmd = StringToByteArray(config.Mac.Replace(":", ""));
                    Array.ConstrainedCopy(tmp_cmd, 0, cmd_hex, i, tmp_cmd.Length);
                    i += tmp_cmd.Length;
                    Array.ConstrainedCopy(end_cmd, 0, cmd_hex, i, end_cmd.Length);
                    i += end_cmd.Length;
                    command = "PW \r\n";
                    tmp_cmd = Encoding.ASCII.GetBytes(command);
                    Array.ConstrainedCopy(tmp_cmd, 0, cmd_hex, i, tmp_cmd.Length);
                    i += tmp_cmd.Length;
                    // The actual Command
                    StringBuilder myStringBuilder = new StringBuilder("");
                    myStringBuilder.Append(BrCmd);
                    myStringBuilder.Append(DbCmd);
                    myStringBuilder.Append(ParCmd);
                    myStringBuilder.Append(SbCmd);
                    myStringBuilder.Append(FlCmd);
                    
                    tmp_cmd = Encoding.ASCII.GetBytes(myStringBuilder.ToString());
                    Array.ConstrainedCopy(tmp_cmd, 0, cmd_hex, i, tmp_cmd.Length);
                    i += tmp_cmd.Length;
                    // Send the command
                    Connection.Write(cmd_hex, 0, i, CommUtils.ProtocolLogFormats.String, out string logText);
                    int partial = Connection.Read(cmd_hex, 0, 15, 1000);

                    // Ack that setting have been apply by writing to XML file the modify value
                    wiznetSerCfg.Modify = false;
                    SaveSerialConfig();
                }
            }

            LoadGpioConfig();
            if (fatalError)
            {
                WriteToLog(state);
            }
            else
            {
                if(wiznetGpioCfg.Modify == true)
                {
                    StringBuilder cmdbuilder = new StringBuilder("");
                    // Apply User Changes to GPIO Configuration
                    // Look Up specific I/O change
                    if(wiznetGpioCfg.GpioA.change == true)
                    {
                        // send command here to adjust GPIO A Setting
                        if (wiznetGpioCfg.GpioA.Mode == "INPUT")
                        {
                            // command to change GPIO to INPUT
                            cmdbuilder.Append("CA0\r\n");
                            // command to read INPUT GPIO to set the current device state in XML file
                            cmdbuilder.Append("GA\r\n");
                        }
                        else {
                            // command to change GPIO to OUTPUT
                            cmdbuilder.Append("CA1\r\n");
                            // Command to write the OUTPUT Value in XML File to device GPIO 
                            cmdbuilder.Append("GA" + wiznetGpioCfg.GpioA.Value.ToString() + "\r\n");
                        }
                        wiznetGpioCfg.GpioA.change = false;
                    }
                    if (wiznetGpioCfg.GpioB.change == true)
                    {
                        // send command here to adjust GPIO B Setting
                        if (wiznetGpioCfg.GpioB.Mode == "INPUT")
                        {
                            // command to change GPIO to INPUT
                            cmdbuilder.Append("CB0\r\n");
                            // command to read INPUT GPIO to set the current device state in XML file
                            cmdbuilder.Append("GB\r\n");
                        }
                        else
                        {
                            // command to change GPIO to OUTPUT  
                            cmdbuilder.Append("CB1\r\n");
                            // Command to write the OUTPUT Value in XML File to device GPIO 
                            cmdbuilder.Append("GB" + wiznetGpioCfg.GpioB.Value.ToString() + "\r\n");
                        }
                        wiznetGpioCfg.GpioB.change = false;
                    }
                    if (wiznetGpioCfg.GpioC.change == true)
                    {
                        // send command here to adjust GPIO C Setting
                        if (wiznetGpioCfg.GpioC.Mode == "INPUT")
                        {
                            // command to change GPIO to INPUT
                            cmdbuilder.Append("CC0\r\n");
                            // command to read INPUT GPIO to set the current device state in XML file
                            cmdbuilder.Append("GC\r\n");
                        }
                        else
                        {
                            // command to change GPIO to OUTPUT
                            cmdbuilder.Append("CC1\r\n");
                            // Command to write the OUTPUT Value in XML File to device GPIO
                            cmdbuilder.Append("GC" + wiznetGpioCfg.GpioC.Value.ToString() + "\r\n");
                        }
                        wiznetGpioCfg.GpioC.change = false;
                    }
                    if (wiznetGpioCfg.GpioD.change == true)
                    {
                        // send command here to adjust GPIO D Setting
                        if (wiznetGpioCfg.GpioD.Mode == "INPUT")
                        {
                            // command to change GPIO to INPUT
                            cmdbuilder.Append("CD0\r\n");
                            // command to read INPUT GPIO to set the current device state in XML file
                            cmdbuilder.Append("GD\r\n");
                        }
                        else
                        {
                            // command to change GPIO to OUTPUT
                            cmdbuilder.Append("CD1\r\n");
                            // Command to write the OUTPUT Value in XML File to device GPIO
                            cmdbuilder.Append("GD"+ wiznetGpioCfg.GpioD.Value.ToString() + "\r\n");
                        }
                        wiznetGpioCfg.GpioD.change = false;
                    }

                    // Build Network Command
                    byte[] cmd_hex = new byte[100]; // not sure how much will use
                    byte[] tmp_cmd;
                    byte[] end_cmd = { 0x0d, 0x0a };
                    int i = 0;
                    // Required for Network Communications
                    string command = "MA";
                    tmp_cmd = Encoding.ASCII.GetBytes(command);
                    Array.ConstrainedCopy(tmp_cmd, 0, cmd_hex, i, tmp_cmd.Length);
                    i += tmp_cmd.Length;
                    tmp_cmd = StringToByteArray(config.Mac.Replace(":", ""));
                    Array.ConstrainedCopy(tmp_cmd, 0, cmd_hex, i, tmp_cmd.Length);
                    i += tmp_cmd.Length;
                    Array.ConstrainedCopy(end_cmd, 0, cmd_hex, i, end_cmd.Length);
                    i += end_cmd.Length;
                    command = "PW \r\n";
                    tmp_cmd = Encoding.ASCII.GetBytes(command);
                    Array.ConstrainedCopy(tmp_cmd, 0, cmd_hex, i, tmp_cmd.Length);
                    i += tmp_cmd.Length;
                    // The actual Command
                    tmp_cmd = Encoding.ASCII.GetBytes(cmdbuilder.ToString());
                    Array.ConstrainedCopy(tmp_cmd, 0, cmd_hex, i, tmp_cmd.Length);
                    i += tmp_cmd.Length;
                    // Send the command
                    Connection.Write(cmd_hex, 0, i, CommUtils.ProtocolLogFormats.String, out string logText);
                    int partial = Connection.Read(cmd_hex, 0, 15, 1000);

                    // Ack Logic processing and save values for User UI
                    wiznetGpioCfg.Modify = false;
                    SaveGpioConfig();
                }
            }

            // finish request
            FinishRequest();

            // generate current data
            for (int i = 0; i < KPTags.Length; i++)
                SetCurData(i, (double)random.Next(1000) / 10.0, 1);

            // calculate stats
            CalcSessStats();
        }

        public override void SendCmd(Command cmd)
        {
            base.SendCmd(cmd);

            if (fatalError)
            {
                WriteToLog(state);
            }
            else
            {

                if ((cmd.CmdNum == 1 || cmd.CmdNum == 2) && cmd.CmdTypeID == BaseValues.CmdTypes.Binary)
                {
                    if (cmd.CmdNum == 1)
                    {
                        // send command data as string
                        // отправка данных команды как строки
                        string logText;
                        Connection.WriteLine(cmd.GetCmdDataStr(), out logText);
                        WriteToLog(logText);
                    }
                    else
                    {
                        // send command data as array of bytes
                        // отправка данных команды как массива байт
                        string logText;
                        Connection.Write(cmd.CmdData, 0, cmd.CmdData.Length, CommUtils.ProtocolLogFormats.Hex,
                            out logText);
                        WriteToLog(logText);
                    }
                }
                else
                {
                    WriteToLog(CommPhrases.IllegalCommand);
                    lastCommSucc = false;
                }
            }

            CalcCmdStats();
        }

        /// <summary>
        /// Perform actions after the connection is established
        /// </summary>
        public override void OnConnectionSet()
        {
            starting = true;
            FileStream fileStream = new FileStream("C:\\StartLog.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.WriteLine("OnConnectionSet\r\n");
            sw.Close();
        }

        /// <summary>
        /// Perform actions after adding KP to the communication line
        /// </summary>
        public override void OnAddedToCommLine()
        {
            
        }

        /// <summary>
        /// Perform actions when starting a communication line
        /// </summary>
        public override void OnCommLineStart()
        {
            LoadConfig();
        }
    }
}
