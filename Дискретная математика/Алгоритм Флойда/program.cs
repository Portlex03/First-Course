class Program
{
    static void Main()
    {
        // чтение матрицы из файла
        string[] strMatrix = File.ReadAllLines("Matrix.txt");
        int n = strMatrix.Length;

        // перевод из строки в числа
        int[,] matrix = new int[n,n];
        for (int i = 0; i < n; i++)
        {
            int[] line = Array.ConvertAll(strMatrix[i].Split(), x => int.Parse(x));
            for (int j = 0; j < n; j++)
            {
                if (line[j] != 0 || i == j) matrix[i, j] = line[j];
                else matrix[i, j] = int.MaxValue / 2;
            }
        }
        // применение алгоритма
        matrix = Floyd.Algorithm(matrix);

        // вывод матрицы
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matrix[i,j] + " ");
            }
            Console.WriteLine();
        }
    }
}
class Floyd
{
    public static int[,] Algorithm(int[,] array)
    {
        int[,] matrixWeight = (int[,])array.Clone();

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int k = 0; k < array.GetLength(0); k++)
                {
                    int a = matrixWeight[j, i];
                    int b = matrixWeight[i, k];
                    int c = matrixWeight[j, k];
                    matrixWeight[j, k] = Math.Min(a + b, c);
                }
            }
        }
        return matrixWeight;
    }
}
