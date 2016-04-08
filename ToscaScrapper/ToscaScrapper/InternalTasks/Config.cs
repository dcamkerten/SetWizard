using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml;

namespace ToscaScrapper.InternalTasks
{
    internal class Config
    {
        public static Hashtable ReadConfig()
        {
            var currentSettings = new Hashtable();

            var path = Environment.ExpandEnvironmentVariables("%TRICENTIS_HOME%") +
                       @"\ToscaCommander\Addins\ScrapperConfig.xml";

            var reader = new StreamReader
                (
                new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
                );

            var xmlIn = reader.ReadToEnd();
            reader.Close();

            var config = new XmlDocument();

            config.LoadXml(xmlIn);


            var xmlNodeList = config.SelectNodes("//scrapperSettings");
            if (xmlNodeList == null) return currentSettings;
            foreach (
                var setting in
                    xmlNodeList.Cast<XmlNode>()
                        .SelectMany(node => node.ChildNodes.Cast<XmlNode>().Where(setting => setting.Attributes != null))
                        .Where(setting => setting.Attributes != null))
            {
                if (setting.Attributes != null)
                    currentSettings.Add
                        (
                            setting.Attributes["key"].Value,
                            setting.Attributes["value"].Value
                        );
            }
            return currentSettings;
        }


        public static void UpdateConfig(Hashtable newSettings)
        {
            var path = Environment.ExpandEnvironmentVariables("%TRICENTIS_HOME%") +
                       @"\ToscaCommander\Addins\ScrapperConfig.xml";


            var reader = new StreamReader
                (
                new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
                );

            var xmlIn = reader.ReadToEnd();
            reader.Close();

            var config = new XmlDocument();

            config.LoadXml(xmlIn);

            var xmlNodeList = config.SelectNodes("//scrapperSettings");
            if (xmlNodeList != null)
                foreach (XmlNode node in xmlNodeList)
                {
                    foreach (XmlNode setting in node.ChildNodes)
                    {
                        if (setting.Attributes != null)
                        {
                            var currentKey = setting.Attributes["key"].Value;

                            if (newSettings.ContainsKey(currentKey))
                            {
                                setting.Attributes["value"].Value = newSettings[currentKey].ToString();
                            }
                        }
                    }
                }
            config.Save(path);
        }
    }
}
