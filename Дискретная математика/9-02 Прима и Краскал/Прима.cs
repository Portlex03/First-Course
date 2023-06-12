internal class Prima
{
    public static void Algorithm(string[] strPeaks, string[] strValues, int n)
    {
        // матрица значений
        int[,] peaks = new int[n, n];
        for (int i = 0; i < strPeaks.Length; i++)
        {
            peaks[int.Parse(strPeaks[i]) / 10, int.Parse(strPeaks[i]) % 10] = int.Parse(strValues[i]);
            peaks[int.Parse(strPeaks[i]) % 10, int.Parse(strPeaks[i]) / 10] = int.Parse(strValues[i]);
        }
        // замена 0 на макс значение
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (peaks[i, j] == 0) peaks[i, j] = int.MaxValue;
            }
        }
        int summ = 0; // сумма
        List<int> indexes = new() { 1 }; // лист с использованными вершинами

        while (indexes.Count != n - 1)
        {
            int min = int.MaxValue;
            int minIndex = 0;
            // минимум всех строк с индексами index[i]
            for (int i = 0; i < indexes.Count; i++)
            {
                int minStroki = int.MaxValue;
                int minIndexStroki = 0;
                // минимум одной строки
                for (int j = 1; j < n; j++)
                {
                    if (peaks[indexes[i], j] < minStroki && !indexes.Contains(j))
                    {
                        minStroki = peaks[indexes[i], j];
                        minIndexStroki = j;
                    }
                }
                // сравнение минимума строки с общим минимумом цикла
                if (minStroki < min)
                {
                    min = minStroki;
                    minIndex = minIndexStroki;
                }
            }
            summ += min; // добавление минимума цикла в сумму
            indexes.Add(minIndex);
        }
        Console.WriteLine(summ);
    }
}
