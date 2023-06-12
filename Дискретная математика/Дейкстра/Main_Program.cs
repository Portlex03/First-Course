using System.Reflection;
using Univercity;

namespace MyNameSpace;

class Main_Program
{
    static void Main()
    {
        // считывание матрицы с файла
        string[] fileMatrix = File.ReadAllLines("files/Матрица.txt");

        int[,] matrix = new int[fileMatrix.Length,fileMatrix.Length];

        for (int i = 0; i < fileMatrix.Length; i++)
        {
            string[] line = fileMatrix[i].Split('\t');

            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] == "0") 
                    matrix[i, j] = int.MaxValue / 2;
                else 
                    matrix[i,j] = int.Parse(line[j]);
            }
        }

        Console.Write("Введите вершины, до которых нужно посчитать минимальный путь: ");
        string[] s = Console.ReadLine().Split();

        int m = int.Parse(s[0]);
        int n = int.Parse(s[1]);

        Дейкстра.Algorithm(matrix,m,n);
    }
}
