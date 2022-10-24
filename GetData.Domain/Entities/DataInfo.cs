using System.Net;
using System.Runtime.InteropServices;
using GetData.Domain.Validation;

namespace GetData.Domain.Entities
{
    public class DataInfo
    {
        public int Id { get; private set; }
        public string LocalIpv4 { get; set; }
        public string Hostname { get; private set; }
        public string PublicIpv4 { get; private set; }
        public string MacAddr { get; private set; }
        public IEnumerable<DataInfo> DataInfos{ get; private set; }
        

        public DataInfo(string localIpv4, string publicIpv4)
        {
            Validate(localIpv4, publicIpv4);
        }
        private void Validate(string localIpv4, string publicIpv4)
        {
            DomainException.When(string.IsNullOrEmpty(localIpv4), 
                "O Ipv4 Local não pode ser vazio ou nulo.");
            DomainException.When(localIpv4.Length > 1000,
                "O Endereço IPv4 Local não pode ser maior que 15.");
            
            DomainException.When(string.IsNullOrEmpty(publicIpv4), 
                "O Ipv4 Publico não pode ser vazio ou nulo.");
            DomainException.When(publicIpv4.Length > 1000,
                "O comprimento do Endereço IPv4 Publico não pode ser maior que 15.");
                /*

            DomainException.When(string.IsNullOrEmpty(hostname),
                "O Hostname não pode ser vazio ou nulo");
            DomainException.When(hostname.Length > 1000,
                "O comprimento do Hostname não pode ser maior que 15.");

            DomainException.When(string.IsNullOrEmpty(macAddr),
                "O MAC não pode ser vazio ou nulo");
            DomainException.When(macAddr.Length > 1000,
                "O comprimento do MAC não pode ser maior que 17.");
                */

            LocalIpv4 = localIpv4;
            PublicIpv4 = publicIpv4;
            /*
            Hostname = hostname;
            MacAddr = macAddr;
            */

        }

        public void GetHostname()
        {
            IPAddress ClientIP;
            string clientDeviceName;
            try
            {
                ClientIP = IPAddress.Parse(LocalIpv4);
                IPHostEntry GetIPHost = Dns.GetHostEntry(ClientIP);
                Hostname = GetIPHost?.HostName?.ToString()?.Split('.')?.ToList()?.FirstOrDefault().Trim();
            } 
            catch (Exception)
            {
                Hostname = "Unknow";
            }
        }

        public void GetMac()
        {
            //Método 1
            string s = string.Empty;
            
            try
            {
                [DllImport("Iphlpapi.dll", ExactSpelling = true)]
                static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);
                System.Net.IPHostEntry Tempaddr = null;
                Tempaddr = (System.Net.IPHostEntry)Dns.GetHostEntry(Hostname);
                System.Net.IPAddress[] TempAd = Tempaddr.AddressList;
                string[] Ipaddr = new string[3];
                foreach (IPAddress TempA in TempAd)
                {
                    Ipaddr[1] = TempA.ToString();
                    byte[] ab = new byte[6];
                    int len = ab.Length;
                    int r = SendARP((int)TempA.Address, 0, ab, ref len);
                    string sMAC = BitConverter.ToString(ab, 0, 6);
                    Ipaddr[2] = sMAC;
                    MacAddr = sMAC.Trim();
                }
            }
            catch (Exception )
            {
                MacAddr = "Unknow";
            }

            if(MacAddr != "Unknow")
                return;

            //Método 2

            [DllImport("Iphlpapi.dll")]
            static extern int SendARPM2(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
            [DllImport("Ws2_32.dll")]
            static extern Int32 inet_addr(string ip);

            string mac_dest = "";
            try
            {
                Int32 ldest = inet_addr(LocalIpv4);
                Int32 lhost = inet_addr("");
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARPM2(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
            }
            catch (Exception err)
            {
                mac_dest = "Unknow";
            }
        }


    }
}