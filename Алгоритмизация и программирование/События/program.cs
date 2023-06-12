class MyEvent
{
    public delegate void MyEventandler();
    public List<MyEventandler> eventList = new();

    public event MyEventandler ActionList
    {
        add
        {
            eventList.Add(value);
        }
        remove
        {
            eventList.Remove(value);
        }
    }

    public void PrintEvents()
    {
        for (int i = 0; i < eventList.Count; i++)
        {
            eventList[i]();
        }
    }
}

class Calculations
{
    public double a, b;

    public Calculations(string a, string b)
    {
        this.a = int.Parse(a);
        this.b = int.Parse(b);
    }
    public void Sum() => Console.WriteLine($"Сумма {a + b}");

    public void Substraction() => Console.WriteLine($"Разность: {a - b}");

    public void Multiply() => Console.WriteLine($"Произведение: {a * b}");

    public void Division()
    {
        if (b != 0)
            Console.WriteLine($"Деление: {a / b}");
        else
            Console.WriteLine("Деление: нельзя делить на 0");
    }
}

class Start
{
    static void Main()
    {
        Console.Write("Введите 2 числа: ");
        string[] input = Console.ReadLine().Split();

        MyEvent @event = new MyEvent();
        Calculations calc = new Calculations(input[0], input[1]);

        @event.ActionList += calc.Sum;
        @event.ActionList += calc.Substraction;
        @event.ActionList += calc.Multiply;
        @event.ActionList += calc.Division;

        @event.PrintEvents();
    }
}




