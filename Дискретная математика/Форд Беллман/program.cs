class Program
{
    static void Main()
    {
        string[] file = File.ReadAllLines("Матрица.txt");

        int[,] matrix = Ford_Bellman.NormalMatrix(file);

        Ford_Bellman.Program(matrix, 0);
    }
}

class Ford_Bellman
{
    public static void Program(int[,] matrix, int index)
    {
        int n = matrix.GetLength(0);

        int[] distance = new int[n];
        Array.Fill(distance, int.MaxValue / 2);
        distance[index] = 0;

        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var a = distance[j];
                    var b = matrix[j, i];
                    var c = distance[i];
                    if (distance[i] > distance[j] + matrix[j, i])
                        distance[i] = distance[j] + matrix[j, i];
                }
            }
        }
        Console.WriteLine("Решение:");
        for (int i = 0; i < distance.Length; i++)
        {
            Console.WriteLine($"Расстояние до {i + 1} вершины: {distance[i]}");
        }
    }
    public static int[,] NormalMatrix(string[] strMatrix)
    {
        int n = strMatrix.Length;
        int[,] matrix = new int[n,n];

        for (int i = 0; i < n; i++)
        {
            int[] line = Array.ConvertAll(strMatrix[i].Split(), x => int.Parse(x));
            for (int j = 0; j < n; j++)
            {
                if (line[j] != 0) matrix[i, j] = line[j];
                else matrix[i, j] = int.MaxValue / 2;
            }
        }
        return matrix;
    }
}
