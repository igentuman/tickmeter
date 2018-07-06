using PcapDotNet.Packets.IpV4;
using System;

namespace tickMeter.Classes
{
    public class PacketFilter
    {
        public string DestIpFilter { get; set; } = "";
        public string SourceIpFilter { get; set; } = "";

        public string DestPortFilter { get; set; } = "";
        public string SourcePortFilter { get; set; } = "";

        public string ProtocolFilter { get; set; } = "";

        public string PacketSizeFilter { get; set; } = "";

        public IpV4Datagram ip;

        protected bool ValidateProtocol()
        {
            switch (ProtocolFilter)
            {
                case "udp":
                    if (ip.Protocol == IpV4Protocol.Udp) return true;
                    break;
                case "tcp":
                    if (ip.Protocol == IpV4Protocol.Tcp) return true;
                    break;
                case "tcp and udp":
                    if (ip.Protocol == IpV4Protocol.Tcp || ip.Protocol == IpV4Protocol.Udp) return true;
                    break;
                case "":
                    return true;
            }
            return false;
        }

        protected bool ValidatePort(string port, int portToCheck)
        {
            if (port == "") return true;

            string[] portParts = port.Split('-');
            if (portParts.Length == 2)
            {
                int fromPort = int.Parse(portParts[0]);
                int toPort = int.Parse(portParts[1]);
                if (portToCheck <= toPort && portToCheck >= fromPort)
                {
                    return true;
                }
                return false;
            }
            portParts = port.Split(',');
            foreach (string checkPort in portParts)
            {
                if (checkPort == portToCheck.ToString())
                    return true;
            }
            return false;
        }

        private bool ValidateIP(string ipFilter, string ip)
        {
            if (ipFilter == "") return true;
            if (ipFilter == ip) return true;
            string[] ipFilterParts = ipFilter.Split('.');
            string[] ipParts = ip.Split('.');
            for (int i = 0; i < ipFilterParts.Length; i++)
            {
                try
                {
                    if (ipFilterParts[i] != ipParts[i]) return false;
                }
                catch (Exception) { return false; }

            }
            return true;
        }

        public bool ValidatePacketSize()
        {
            int packetSize = ip.Length;
            if (PacketSizeFilter == "") return true;
            string[] sizes = PacketSizeFilter.Split(',');
            if(sizes.Length > 1)
            {
                foreach(string sizeP in sizes)
                {
                    if(int.Parse(sizeP) == packetSize)
                    {
                        return true;
                    }
                }
                return false;
            }

            sizes = PacketSizeFilter.Split('-');
            if (sizes.Length == 2)
            {
                if(int.Parse(sizes[0]) <= packetSize && int.Parse(sizes[1]) >= packetSize)
                {
                    return true;
                }
                return false;
            }

            if (int.Parse(PacketSizeFilter) == packetSize) return true;

            return false;
        }

        public bool Validate()
        {
            if (!ValidateProtocol()) return false;

            string SourcePort = "";
            string DestPort = "";

            if (ip.Protocol == IpV4Protocol.Udp)
            {
                SourcePort = ip.Udp.SourcePort.ToString();
                DestPort = ip.Udp.DestinationPort.ToString();
            }
            else if (ip.Protocol == IpV4Protocol.Tcp)
            {
                SourcePort = ip.Tcp.SourcePort.ToString();
                DestPort = ip.Tcp.DestinationPort.ToString();
            }
            if (!ValidatePort(SourcePortFilter, int.Parse(SourcePort))) return false;

            if (!ValidatePort(DestPortFilter, int.Parse(DestPort))) return false;

            string SourceIp = ip.Source.ToString();
            string DestIp = ip.Destination.ToString();

            if (!ValidateIP(SourceIpFilter, SourceIp)) return false;
            if (!ValidateIP(DestIpFilter, DestIp)) return false;

            if (!ValidatePacketSize()) return false;

            return true;
        }
    }
}
