/*
 * Copyright 2016 Mikhail Shiryaev
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
 * Module   : KpEmail
 * Summary  : Mail server connection configuration
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2016
 * Modified : 2016
 */

using Scada.Comm.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Scada.Comm.Devices.KpWiznet
{
    /// <summary>
    /// Mail server connection configuration
    /// <para>Конфигурация соединения с почтовым сервером</para>
    /// </summary>
    internal class Config
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Config()
        {
            SetToDefault();
        }

        ///// <summary>
        ///// Получить или установить соединение с физическим КП
        ///// </summary>
        //public static Connection Conn = null;

        /// <summary>
        /// Wiznet MAC Address
        /// </summary>
        public string Mac { get; set; }

        /// <summary>
        /// Configuration Port
        /// </summary>
        public int Port { get; set; }
        
        /// <summary>
        /// Device Network Settings
        /// </summary>
        public string IPaddr { get; set; }
        public string Mask { get; set; }
        public string Gateway { get; set; }
        public bool DHCP { get; set; }  // true means enable DHCP - same as embedded device command

        /// <summary>
        /// Device General Settings
        /// </summary>
        public string ProductName { get; set; }
        public string Firmware { get; set; }
        public bool PollingCfg { get; set; }


        /// <summary>
        /// Set default configuration parameter values
        /// </summary>
        private void SetToDefault()
        {
            Mac = System.String.Empty;
            Port = 50001;
            IPaddr = System.String.Empty;
            Mask = System.String.Empty;
            Gateway = System.String.Empty;
            DHCP = true;
            PollingCfg = true;
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
                Mac = rootElem.GetChildAsString("Mac");
                Port = rootElem.GetChildAsInt("Port");
                IPaddr = rootElem.GetChildAsString("IPaddr");
                Mask = rootElem.GetChildAsString("Mask");
                Gateway = rootElem.GetChildAsString("Gateway");
                DHCP = rootElem.GetChildAsBool("DHCP");
                ProductName = rootElem.GetChildAsString("ProductName");
                Firmware = rootElem.GetChildAsString("Firmware");
                PollingCfg = rootElem.GetChildAsBool("PollingCfg");

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

                XmlElement rootElem = xmlDoc.CreateElement("KpWiznetConfig");
                xmlDoc.AppendChild(rootElem);

                rootElem.AppendElem("Mac", Mac);
                rootElem.AppendElem("Port", Port);
                rootElem.AppendElem("IPaddr", IPaddr);
                rootElem.AppendElem("Mask", Mask);
                rootElem.AppendElem("Gateway", Gateway);
                rootElem.AppendElem("DHCP", DHCP);
                rootElem.AppendElem("ProductName", ProductName);
                rootElem.AppendElem("Firmware", Firmware);
                rootElem.AppendElem("PollingCfg", PollingCfg);

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
            return configDir + "KpWiznet_" + CommUtils.AddZeros(kpNum, 3) + ".xml";
        }
    }
}
