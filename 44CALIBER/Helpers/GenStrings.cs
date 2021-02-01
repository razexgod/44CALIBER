using System;

namespace youknowcaliber
{
    class GenStrings
    {
        public static string Generate()
        {
            string abc = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = "";
            Random rnd = new Random();
            int iter = rnd.Next(0, abc.Length);
            for (int i = 0; i < iter; i++)
                result += abc[rnd.Next(10, abc.Length)];
            return result;

        }

        public static int GenNumbersTo()
        {
            //Создание объекта для генерации чисел
            Random rnd = new Random();
            //Получить случайное число (в диапазоне от 11 до 99)
            int value = rnd.Next(11, 99);
            return value;
        }
    }
}
