using IniParser;
using IniParser.Model;
using PcapDotNet.Packets;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

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

        public void ProcessPacket(Packet packet)
        {
            if (!isEnabled) return;
            profileFilter.ip = packet.Ethernet.IpV4;
            if (profileFilter.Validate())
            {
                if(packet.Ethernet.IpV4.Destination.ToString() == App.meterState.LocalIP)
                {
                    if (App.meterState.tickTimeBuffer.Count > 512)
                    {
                        App.meterState.tickTimeBuffer.RemoveAt(0);
                    }
                    if(App.meterState.CurrentTimestamp != null)
                    {
                        float tickTime = (float)packet.Timestamp.Subtract(App.meterState.CurrentTimestamp).TotalMilliseconds;
                        App.meterState.tickTimeBuffer.Add(tickTime);
                    }
                    
                    App.meterState.CurrentTimestamp = packet.Timestamp;
                    App.meterState.Game = GameName;
                    App.meterState.Server.Ip = packet.Ethernet.IpV4.Source.ToString();
                    App.meterState.DownloadTraffic += packet.Ethernet.IpV4.TotalLength;
                    App.meterState.TickRate++;
                    App.meterState.Server.PingPort = packet.Ethernet.IpV4.Udp.SourcePort;
                    return;
                } 
            }
            // validate packet sending
            if(profileFilter.ValidateForOutputPacket())
            {
                if (packet.Ethernet.IpV4.Source.ToString() == App.meterState.LocalIP)
                {
                    App.meterState.UploadTraffic += packet.Ethernet.IpV4.TotalLength;
                }
            }
        }

        public void Save()
        {
            parser = new FileIniDataParser();
            if (!Directory.Exists("profiles"))
            {
                Directory.CreateDirectory("profiles");
            }
            string ProfileName = GameName;

            try
            {
                File.WriteAllText("profiles/" + ProfileName + ".ini", "[PROFILE_CONFIG]" + Environment.NewLine);

            }
            catch (Exception) { MessageBox.Show(string.Format("Не могу сохранить профиль: {0}.ini", ProfileName)); }

            data = parser.ReadFile("profiles/" + ProfileName + ".ini");
            SetOption("from_port_filter", profileFilter.SourcePortFilter);
            SetOption("to_port_filter", profileFilter.DestPortFilter);
            SetOption("from_ip_filter", profileFilter.SourceIpFilter);
            SetOption("to_ip_filter", profileFilter.DestIpFilter);
            SetOption("packet_size_filter", profileFilter.PacketSizeFilter);
            SetOption("process_filter", profileFilter.ProcessFilter);
            SetOption("protocol_filter", profileFilter.ProtocolFilter);
            SetOption("require_process", RequireProcess);
            SetOption("is_active", isEnabled.ToString());
            SetOption("profile_name", ProfileName);
            SaveConfig(ProfileName);
        }

        public string GetOption(string optionName, string scope = "PROFILE_CONFIG")
        {

            if (data[scope] != null && data[scope][optionName] != "")
            {
                return data[scope][optionName];
            }
            return "";
        }

        public void SetOption(string optionName, string value, string scope = "PROFILE_CONFIG")
        {
            if (data[scope] == null) return;
            data[scope][optionName] = value;

        }

        public void SaveConfig(string ProfileName)
        {
            try
            {
                parser.WriteFile("profiles/" + ProfileName + ".ini", data);
            }
            catch (Exception) { MessageBox.Show("Не могу сохранить профиль. Не хватает прав на запись."); }
        }

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
            profileFilter.ProtocolFilter = GetOption("protocol_filter");
            RequireProcess = GetOption("require_process");
            if (RequireProcess == "") RequireProcess = profileFilter.ProcessFilter;
            GameName = GetOption("profile_name");

            isEnabled = GetOption("is_active") == "True";
        }

       
    }
}
