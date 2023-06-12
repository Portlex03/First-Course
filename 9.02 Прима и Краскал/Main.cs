namespace Прима_и_Краскал;
class Program
{
    static void Main()
    {
        StreamReader f = new StreamReader(@"C:\\Users\\Azerty\\Desktop\\Ввод.txt");
        int n = int.Parse(f.ReadLine()) + 1; // кол-во вершин
        string[] strPeaks = f.ReadLine().Split(); // вершины
        string[] strValues = f.ReadLine().Split(); // веса

        Prima.Algorithm(strPeaks,strValues, n);
        Kraskal.Algorithm(strPeaks, strValues, n);
    }
}
