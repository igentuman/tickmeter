using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace tickMeter
{
    public class SettingsManager
    {
        FileIniDataParser parser;
        IniData data;

        public SettingsManager()
        {
            parser = new FileIniDataParser();
            if (!File.Exists("settings.ini"))
            {
                File.Create("settings.ini");
                File.WriteAllText("settings.ini", "[SETTINGS]"+Environment.NewLine);
            }
            data = parser.ReadFile("settings.ini");
        }

        public string GetOption(string optionName,string scope = "SETTINGS")
        {

            if (data[scope] != null && data[scope][optionName] != "")
            {
                return data[scope][optionName];
            }
            return "";
        }

        public void SetOption(string optionName, string value, string scope = "SETTINGS")
        {
            if (data[scope] == null) return;
            data[scope][optionName] = value;
            
        }

        public void SaveConfig()
        {
            try { 
                parser.WriteFile("settings.ini", data);
            } catch(Exception) { MessageBox.Show("Не могу сохранить настройки. Не хватает прав на запись."); }
}
    }
}
