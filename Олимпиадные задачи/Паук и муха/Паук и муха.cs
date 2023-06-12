class Program
{
    static void Main()
    {
        for (int i = 1; i <= 20; i++)
        {
            // файл с данными
            StreamReader f = new StreamReader($@"C:\Users\Azerty\Desktop\Debug\Паук и муха\input_s1_{i}.txt");
            // файл с ответом
            StreamReader f1 = new StreamReader($@"C:\Users\Azerty\Desktop\Debug\Паук и муха\output_s1_{i}.txt");
            // считывание параметров комнаты
            string[] s1 = f.ReadLine().Split();
            int a = int.Parse(s1[0]);
            int b = int.Parse(s1[1]);
            int c = int.Parse(s1[2]);

            // считывание координат паука
            string[] s2 = f.ReadLine().Split();
            int Sx = int.Parse(s2[0]);
            int Sy = int.Parse(s2[1]);
            int Sz = int.Parse(s2[2]);

            // считывание координат мухи
            string[] s3 = f.ReadLine().Split();
            int Fx = int.Parse(s3[0]);
            int Fy = int.Parse(s3[1]);
            int Fz = int.Parse(s3[2]);

            // вычисление кратчайшего пути:
            double len = double.MaxValue;
            double[] lens = new double[4];
            // паук и муха находятся на параллельных стенах
            if (Math.Abs(Fz - Sz) == c)
            {
                lens[0] = Math.Sqrt(Math.Pow(Fx - Sx, 2) + Math.Pow(2 * b - Fy - Sy + c, 2));
                lens[1] = Math.Sqrt(Math.Pow(Fx - Sx, 2) + Math.Pow(Fy + Sy + c, 2));
                lens[2] = Math.Sqrt(Math.Pow(Fy - Sy, 2) + Math.Pow(2 * a - Fx - Sx + c, 2));
                lens[3] = Math.Sqrt(Math.Pow(Fy - Sy, 2) + Math.Pow(Fx + Sx + c, 2));
                len = lens.Min();
            }
            if (Math.Abs(Fx - Sx) == a)
            {
                lens[0] = Math.Sqrt(Math.Pow(Fz - Sz, 2) + Math.Pow(2 * b - Fy - Sy + a, 2));
                lens[1] = Math.Sqrt(Math.Pow(Fz - Sz, 2) + Math.Pow(Fy + Sy + a, 2));
                lens[2] = Math.Sqrt(Math.Pow(Fy - Sy, 2) + Math.Pow(2 * c - Fz - Sz + a, 2));
                lens[3] = Math.Sqrt(Math.Pow(Fy - Sy, 2) + Math.Pow(Fz + Sz + a, 2));
                len = Math.Min(lens.Min(), len);
            }
            if (Math.Abs(Fy - Sy) == b)
            {
                lens[0] = Math.Sqrt(Math.Pow(Fx - Sx, 2) + Math.Pow(2 * c - Fz - Sz + b, 2));
                lens[1] = Math.Sqrt(Math.Pow(Fx - Sx, 2) + Math.Pow(Fz + Sz + b, 2));
                lens[2] = Math.Sqrt(Math.Pow(Fz - Sz, 2) + Math.Pow(2 * a - Fx - Sx + b, 2));
                lens[3] = Math.Sqrt(Math.Pow(Fz - Sz, 2) + Math.Pow(Fx + Sx + b, 2));
                len = Math.Min(lens.Min(),len);
            }
            // паук и муха находятся на смежных стенах / одной стене
            else
            {
                int[] array_A = new int[2] { 0, a };
                int[] array_B = new int[2] { 0, b };
                int[] array_C = new int[2] { 0, c };
                double x = Math.Abs(Fx - Sx);
                double y = Math.Abs(Fy - Sy);
                double z = Math.Abs(Fz - Sz);
                if (!array_A.Contains(Fx) && !array_A.Contains(Sx) || array_B.Contains(Fy) && array_B.Contains(Sy)) 
                    len = Math.Sqrt(x * x + Math.Pow(y + z, 2));
                else if (!array_B.Contains(Fy) && !array_B.Contains(Sy) || array_C.Contains(Fz) && array_C.Contains(Sz)) 
                    len = Math.Sqrt(y * y + Math.Pow(x + z, 2));
                else if (!array_C.Contains(Fz) && !array_C.Contains(Sz) || array_A.Contains(Fx) && array_A.Contains(Sx)) 
                    len = Math.Sqrt(z * z + Math.Pow(x + y, 2));
            }
            // сравнение ответов
            Console.WriteLine($"{i}) {Math.Round(len, 3)} и {f1.ReadLine()}");
        }
    }
}