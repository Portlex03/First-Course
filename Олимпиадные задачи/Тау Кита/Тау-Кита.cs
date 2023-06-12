class Program
{
    static void Main()
    {
        for (int k = 1; k < 10; k++)
        {
            StreamReader f = new StreamReader($@"C:\Users\Azerty\Desktop\Debug\Тау - Кита\input_s1_{k}.txt");
            string[] words = f.ReadLine().Split();
            int n = words.Length;
            string finallyStroka = "";

            int index1 = n / 2;
            for (int i = 0; i < n; i++)
            {
                string startWord = words[index1];
                index1 += (int)Math.Pow(-1, i + 1) * (i + 1);

                int index2 = startWord.Length / 2;
                string endWord = "";

                for (int j = 0; j < startWord.Length; j++)
                {
                    endWord += startWord[index2];
                    index2 += (int)Math.Pow(-1, j + 1) * (j + 1);
                }
                finallyStroka += endWord + " "; // ответ программы
            }
            // сравнение ответов программы и реального ответа
            StreamReader f1 = new StreamReader($@"C:\Users\Azerty\Desktop\Debug\Тау - Кита\output_s1_{k}.txt");
            string realAswer = f1.ReadLine() + " "; // реальный ответ
            if (finallyStroka == realAswer) Console.WriteLine($"{k}) Успешно");
            else Console.WriteLine($"{k}) Ошибка");
        }
    }
}
