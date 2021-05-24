using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using System.IO;

namespace BBI.JD
{
    public static class Config
    {
        static string APPDATA_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string CFGFOLDER_PATH = Path.Combine(APPDATA_PATH, "BBI LegendGenerator");
        static string CFGFILE_PATH = Path.Combine(CFGFOLDER_PATH, "LegendGenerator.config");

        public static string Get(string key)
        {
            Configuration config = GetConfiguration();

            if (config != null)
            {
                KeyValueConfigurationElement element = config.AppSettings.Settings[key];

                if (element != null)
                {
                    return element.Value;
                }
            }

            return string.Empty;
        }

        public static void Set(string key, string value)
        {
            Configuration config = GetConfiguration();

            if (config != null)
            {
                KeyValueConfigurationElement element = config.AppSettings.Settings[key];

                if (element != null)
                {
                    element.Value = value;

                    config.Save(ConfigurationSaveMode.Modified);
                }
            }
        }

        private static Configuration GetConfiguration()
        {
            Configuration config = null;

            try
            {
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = CFGFILE_PATH;

                config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            }
            catch (Exception ex) { }

            return config;
        }
    }
}
