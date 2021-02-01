using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace youknowcaliber
{
    class BSSID
    {
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);
        public static string GetBSSID()
        {
            byte[] macAddr = new byte[6];
            uint macAddrLen = (uint)macAddr.Length;
            try
            {
                string ip = GetDefaultGateway();
                if (SendARP(BitConverter.ToInt32(IPAddress.Parse(ip).GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
                {
                    return "unknown";
                }
                else
                {
                    string[] v = new string[(int)macAddrLen];
                    for (int j = 0; j < macAddrLen; j++)
                        v[j] = macAddr[j].ToString("x2");
                    return string.Join(":", v);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return "Failed";
        }
        public static string GetDefaultGateway()
        {
            try
            {
                return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address)
                .Where(a => a != null)
                .FirstOrDefault()
                .ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return "Unknown";
        }
    }
}
