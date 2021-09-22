using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Management;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Info i = new Info();

            string text = i.GetOSInfo() + "\n" + i.GetComputerName() + "\n" + i.GetTimeZone() + "\n" + i.GetNETversion();
            try {


                TcpClient client = new TcpClient("127.0.0.1", 4444);
               
                Console.WriteLine("Connected!");
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.Unicode.GetBytes(text);
                
               
                Console.WriteLine("Sending : " + text);
                stream.Write(data, 0, data.Length);

                // закрываем сокет
                client.Close();
                Console.Read();
                
            }
           
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                Console.Read();
            }
        } 
    }
    public class Info
    {
        public string GetOSInfo()
        {
            string result = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                result = os["Caption"].ToString();
                break;
            }
            return result;
        }
        public string GetComputerName()
        {
            return "Computer name: " + Environment.MachineName;
        }
        public string GetTimeZone()
        {
            return "Current Time Zone: " + TimeZoneInfo.Local.DisplayName;
        }
        public string GetNETversion()
        {
            return ".NET version: " + Environment.Version;
        }
    }
}
