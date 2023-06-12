namespace Прима_и_Краскал;

internal class Kraskal
{
    public static void Algorithm(string[] strPeaks, string[] strValues, int n)
    {
        // веса
        int[] values = new int[strValues.Length];
        for (int i = 0; i < strValues.Length; i++)
        {
            values[i] = int.Parse(strValues[i]);
        }
        // Сортировка
        for (int i = 0; i < values.Length - 1; i++)
        {
            for (int j = 0; j < values.Length - 1; j++)
            {
                if (values[j] > values[j + 1])
                {
                    int t1 = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = t1;

                    string t2 = strPeaks[j];
                    strPeaks[j] = strPeaks[j + 1];
                    strPeaks[j + 1] = t2;
                }
            }
        }
        //поиск суммы
        int summ = 0;
        List<string> indexes = new(); // список списков вершин 

        for (int i = 0; i < values.Length; i++)
        {
            string s1 = Convert.ToString(strPeaks[i][0]); // первая вершина
            string s2 = Convert.ToString(strPeaks[i][1]); // вторая вершина
            bool flag = true;
            int count = 0; // кол-во списков, в которых могут находится две вершины
            List<int> set = new();
            for (int j = 0; j < indexes.Count; j++)
            {
                // если вершины содержатся в каком - либо списке вершин
                if (indexes[j].Contains(s1) && indexes[j].Contains(s2))
                {
                    flag = false;
                    break;
                }
                // если одна из вершин содержится в каком-либо списке вершин
                if (indexes[j].Contains(s1) || indexes[j].Contains(s2))
                {
                    flag = false;
                    count++;
                    set.Add(j);
                }
            }
            // добавление вершины в список
            if (count == 1)
            {
                summ += values[i];
                if (indexes[set[0]].Contains(s1)) indexes[set[0]] += s2;
                else indexes[set[0]] += s1;
            }
            // объединение списков вершин
            else if (count == 2)
            {
                summ += values[i];
                indexes[set[0]] += indexes[set[1]];
                indexes.Remove(indexes[set[1]]);
            }
            // создание нового списка вершин
            if (flag)
            {
                summ += values[i];
                indexes.Add(s1 + s2);
            }
        }
        Console.WriteLine(summ);
    }
}
