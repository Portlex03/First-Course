using static Calculator.IMath;

namespace Calculator;

interface IMath
{
    void Sum();
    void Subtraction();
    void Multiplication();
    void Division();

    delegate void Operation();
}

class Calculator :IMath
{
    double a, b;

    Operation _Operation;

    public Calculator(string? a, string? b)
    {
        if (!double.TryParse(a, out this.a) && !double.TryParse(b, out this.b))
        {
            Console.WriteLine("\nНеверно введены оба числа");
            TryAgain();
        }
        else if (!double.TryParse(a, out this.a))
        {
            Console.WriteLine("\nНеверно введено 1 число");
            TryAgain();
        }
            
        else if (!double.TryParse(b, out this.b))
        {
            Console.WriteLine("\nНеверно введено 2 число");
            TryAgain();
        }
            
    }

    public void Sum()
    {
        Console.WriteLine($"\nОтвет: {a + b}");
    }

    public void Subtraction()
    {
        Console.WriteLine($"\nОтвет: {a - b}");
    }

    public void Multiplication()
    {
        Console.WriteLine($"\nОтвет: {a * b}");
    }

    public void Division()
    {
        if (b == 0)
        {
            Console.WriteLine("\nОшибка: Нельзя делить на 0");
            TryAgain();
        }
        else
            Console.WriteLine($"\nОтвет: {a / b}");
    }

    public void ChouseOperation(string? chouse)
    {
        switch(chouse)
        {
            case "1": _Operation += Sum; break;
            case "2": _Operation += Subtraction; break;
            case "3": _Operation += Multiplication; break;
            case "4": _Operation += Division; break;
            default:
                Console.WriteLine("\nОшибка, неверно выбрана операция");
                TryAgain();
                break;
        }
    }

    public static void TryAgain()
    {
        Console.Write("\nПопробовать ещё раз (нажмите любую кнопку): ");
        Console.ReadKey();
        Console.Clear();
        Main();
    }

    static void Main()
    {
        Console.WriteLine("Калькулятор");

        Console.Write("\nВведите 1 число: ");
        string? a = Console.ReadLine();

        Console.Write("\nВведите 2 число: ");
        string? b = Console.ReadLine();

        var calc = new Calculator(a,b);

        Console.WriteLine("\nВыберите действие:\n" + "1: Сложение\n" + "2: Вычитание\n" + "3: Умножение\n" + "4: Деление\n");
        string? chouse = Console.ReadLine();

        calc.ChouseOperation(chouse);
        calc._Operation();
    }
}
