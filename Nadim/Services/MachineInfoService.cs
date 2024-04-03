using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Services
{
    public static class MachineInfoService
    {
        public static IPAddress GetExternalIpAddress()
        {
            using (var client = new HttpClient())
            {
                var externalIpString = client.GetStringAsync("http://icanhazip.com").Result
                    .Replace("\r\n", "").Replace("\n", "").Trim();

                if (IPAddress.TryParse(externalIpString, out var ipAddress))
                {
                    return ipAddress;
                }

            }

            return null;
        }

        public static string GetLocalIPAddress()
        {
            // Get the hostname
            string hostname = Dns.GetHostName();

            // Get the IP address list associated with the hostname
            IPAddress[] ipList = Dns.GetHostAddresses(hostname);

            // Filter out loopback addresses (127.0.0.1 and ::1)
            IPAddress localIP = Array.Find(ipList, ip => !ip.IsIPv6LinkLocal && !ip.IsIPv6SiteLocal);

            // Return the first non-loopback IP address
            return localIP?.ToString();
        }

        public static string GetComputerName()
        {
            string model = null;
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_ComputerSystem"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        model = obj["Model"]?.ToString();
                        break; // Get the first result (may return multiple)
                    }
                }
            }
            catch (Exception) { } // Handle potential exceptions
            return Environment.MachineName + " - " + model;
        }
    }
}
