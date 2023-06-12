class Tree
{
    int location;
    int sum;
    readonly int countPicks;
    readonly int countBerries;
    readonly int minSpel;
    readonly int[,] matrix;
    readonly List<int> berries = new();
    
    public Tree(int countPicks, int countBerries, string[] data)
    {
        this.countPicks = countPicks;
        this.countBerries = countBerries;

        // заполнение матрицы
        matrix = new int[countPicks, countPicks];
        FillMatrix(data);

        // позиция и минимальная спелость ягод
        int[] lastLine = Array.ConvertAll(data[^1].Split(), int.Parse);
        location = lastLine[0];
        minSpel = lastLine[1];

        // заполнение ягод
        string[] dataCopy = new string[countBerries];
        Array.Copy(data, countPicks, dataCopy, 0, dataCopy.Length);
        FillBerries(dataCopy);
    }

    void FillMatrix(string[] data)
    {
        for (int i = 1; i < countPicks; i++)
        {
            int[] peakAndValue = Array.ConvertAll(data[i].Split(), int.Parse);

            int peak = peakAndValue[0];
            int value = peakAndValue[1];

            matrix[i, peak] = value;
            matrix[peak, i] = value;

            for (int j = 0; j < i; j++)
            {
                if (matrix[i,j] == 0)
                {
                    matrix[i,j] = value + matrix[peak, j];
                    matrix[j, i] = value + matrix[peak, j];
                }
            }
        }
    }

    void FillBerries(string[] data)
    {
        for (int i = 0; i < countBerries; i++)
        {
            int[] peakAndValue = Array.ConvertAll(data[i].Split(), int.Parse);

            int peak = peakAndValue[0];
            int value = peakAndValue[1];

            if (value >= minSpel)
                berries.Add(peak);
        }
        Console.WriteLine(berries.Count);
    }

    public int GiveAnswer()
    {
        while (berries.Count > 0)
        {
            (int minWay, location) = Find_MinWay(berries);
            sum += minWay;
            berries.Remove(location);
        }
        return sum;
    }

    (int,int) Find_MinWay(List<int> way)
    {
        int[] minWays = new int[way.Count];
        for (int i = 0; i < way.Count; i++)
        {
            minWays[i] = matrix[location, way[i]];
        }
        var element = minWays.Min();
        var index = Array.IndexOf(minWays, element);
        index = berries[index];

        return (element, index);
    }

    public void Print()
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"{matrix[i, j],3} ");
            }
            Console.WriteLine();
        }
    }
}

class StartProgram
{
    static void Main()
    {
        for (int i = 23; i < 26; i++)
        {
            string[] data = File.ReadAllLines($"input_s1_{i}.txt");

            string[] firstLine = data[0].Split();
            int countPeaks = int.Parse(firstLine[0]) + 1;
            int countBerries = int.Parse(firstLine[1]);

            var tree = new Tree(countPeaks, countBerries, data);
            int myAnswer = tree.GiveAnswer();

            int realAnswer = int.Parse(File.ReadAllLines($"output_s1_{i}.txt")[0]);
            if (myAnswer == realAnswer) 
                Console.WriteLine($"{i}: {myAnswer == realAnswer}");
            else
                Console.WriteLine($"{i}: {myAnswer} {realAnswer}");
        }
        
    }
}
