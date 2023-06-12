using System.Threading.Channels;

class Calculations
{
    public Calculations(int a, int b, int c)
    {
        this.a = a;
        this.b = b;
        this.c = c;

        operation += Min;
        operation += Max;
        operation += Sum;
        operation += Multiply;
        operation += Average;
    }

    void Min() => Console.WriteLine($"Максимум: {Math.Min(Math.Min(a, b), c)}");
    void Max() => Console.WriteLine($"Минимум: {Math.Max(Math.Max(a, b), c)}");
    void Sum() => Console.WriteLine($"Сумма: {a + b + c}");
    void Multiply() => Console.WriteLine($"Произведение: {a * b * c}");
    void Average() => Console.WriteLine($"Среднее значение: {(a + b + c) / 3}");

    double a, b, c;
    public Operation operation;
    public delegate void Operation();

    static void Main(string[] args)
    {
        Console.Write("Введите 3 числа: ");
        int[] s = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        var calc = new Calculations(s[0], s[1], s[2]);
        calc.operation();
    }
}
