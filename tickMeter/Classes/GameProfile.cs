using IniParser;
using IniParser.Model;
using System;
using System.Diagnostics;

namespace tickMeter.Classes
{
    public class GameProfile
    {
        public PacketFilter profileFilter;
        public string GameName;
        public string RequireProcess;
        public bool isEnabled;
        public IniData data;
        public FileIniDataParser parser;

        public static GameProfile InitFile(string fileName)
        {
            
            
            GameProfile gProfile = new GameProfile();
            gProfile.profileFilter = new PacketFilter();
            try
            {
                gProfile.readConfig(fileName);
            } catch(Exception) {
                gProfile.isEnabled = false;
                Debug.Print("profile: " + fileName + " load error");
            }

            return gProfile;
        }

        public void readConfig(string fileName)
        {
            parser = new FileIniDataParser();
            data = parser.ReadFile(fileName);
            profileFilter.DestIpFilter = GetOption("to_ip_filter");
            profileFilter.DestPortFilter = GetOption("to_port_filter");
            profileFilter.SourceIpFilter = GetOption("from_ip_filter");
            profileFilter.SourcePortFilter = GetOption("from_port_filter");
            profileFilter.PacketSizeFilter = GetOption("packet_size_filter");
            profileFilter.ProcessFilter = GetOption("process_filter");
            RequireProcess = GetOption("require_process");
            if (RequireProcess == "") RequireProcess = profileFilter.ProcessFilter;
            GameName = RequireProcess;

            isEnabled = GetOption("is_active") == "True";
        }

        public string GetOption(string optionName, string scope = "PROFILE_CONFIG")
        {

            if (data[scope] != null && data[scope][optionName] != "")
            {
                return data[scope][optionName];
            }
            return "";
        }
    }
}
