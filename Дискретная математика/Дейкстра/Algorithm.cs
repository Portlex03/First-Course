namespace Univercity;

internal class Дейкстра
{
    public static void Algorithm(int[,] matrix, int p1, int p2)
    {
        Console.Write($"Расстояние от вершины {p1} до вершины {p2}: ");
        p1--; p2--;

        // количество вершин
        int n = matrix.GetLength(0);

        // массив с минимумами столбцов 
        int[] MinColums = new int[n];

        // заполнение массива максимальными числами
        for ( int i = 0; i < n; i++) MinColums[i] = int.MaxValue / 2;

        // минимальный путь от вершины p1 до вершины p2
        int len = 0;

        // список использованных вершин
        List<int> numbers = new() { p1 };

        while (p1 != p2)
        {
            // копия строки массива
            int[] line = new int[n];

            // заполнение массива максимальными числами
            for (int i = 0; i < n; i++) line[i] = int.MaxValue / 2;

            // проходимся по строке массива с номером p1
            for (int i = 0; i < n; i++)
            {
                if (!numbers.Contains(i))
                {
                    line[i] = Math.Min(matrix[p1, i] + len, MinColums[i]);

                    MinColums[i] = Math.Min(line[i], MinColums[i]);
                }
            }
            len = line.Min();
            p1 = Array.IndexOf(line,len);
            numbers.Add(p1);
        }
        Console.WriteLine($"Ответ {len}");
    }
}
