namespace Learning;
using PersonalMenu;

internal static class MyStack
{
    readonly static Stack<char> _Stack = new();

    static MyStack()
    {
        _Stack.Fill();
    }

    delegate void Functions();

    public static string[] text =
    {
        "1: Contains",
        "2: Peek",
        "3: Pop",
        "4: Push",
        "5: TryPeek",
        "6: TryPop",
        "7: Задачка со стеком",
        "8: Выход в меню"
    };

    public static void Choising(int number)
    {
        Console.Clear();
        var Functions = new List<Functions>()
        {
            Contains, Peek, Pop,
            Push, TryPeek, TryPop,Task
        };
        if (number == Functions.Count) Project.MenuExit(Project.MenulastValue);
        else
        {
            Functions[number]();
            Project.Exit(number);
        }
    }

    static void Fill(this Stack<char> stack)
    {
        Random number = new Random();

        int len = number.Next(5, 20);

        for (int i = 0; i < len; i++)
        {
            char key = (char)number.Next(65, 91);
            stack.Push(key);
        }
    }

    //1
    static void Contains()
    {
        Paint.Title("Функция Contains");
        Paint.SubTitle("\nИсходная коллекция:");
        _Stack.Print();

        Paint.Input("\nВведите элемент: ");
        char element = char.Parse(Console.ReadLine());

        if (_Stack.Contains(element))
            Paint.SubTitle($"\nЭлемент {element} содержится в коллекции");
        else
            Paint.Error("\nЭлемента нет в коллекции");
    }

    //2
    static void Peek()
    {
        Paint.Title("Функция Push");
        Paint.SubTitle("\nИсходная коллекция:");
        _Stack.Print();

        char element = _Stack.Pop();
        Paint.SubTitle($"\n Первый элемент коллекции: {element}. Стек после функции Peek:");
        _Stack.Print();
    }

    //3
    static void Pop()
    {
        Paint.Title("Функция Pop");
        Paint.SubTitle("\nИсходная коллекция:");
        _Stack.Print();

        char element = _Stack.Peek();
        Paint.SubTitle($"\n Первый элемент коллекции: {element}. Стек после функции Pop:");
        _Stack.Print();
    }

    //4
    static void Push()
    {
        Paint.Title("Функция Push");
        Paint.SubTitle("\nИсходная коллекция:");
        _Stack.Print();

        Paint.Input("\nВведите элемент: ");
        char element = char.Parse(Console.ReadLine());

        Paint.SubTitle("\nСтек после добавления элемента:");
        _Stack.Push(element);
        _Stack.Print();
    }
    //5
    static void TryPeek()
    {
        Paint.Title("Функция TryPeek");
        Paint.SubTitle("\nИсходная коллекция:");
        _Stack.Print();

        char result;
        if (_Stack.TryPeek(out result))
        {
            Paint.SubTitle($"\nУдалось извлечь элемент {result} из стека");
            Paint.SubTitle("\nСтек после функции TryPeek:");
            _Stack.Print();
        }
        else
            Paint.Error($"\nНе удалось извлечь элемент из стека");
    }

    //6
    static void TryPop()
    {
        Paint.Title("Функция TryPop");
        Paint.SubTitle("\nИсходная коллекция:");
        _Stack.Print();

        char result;
        if (_Stack.TryPop(out result))
        {
            Paint.SubTitle($"\nУдалось извлечь элемент {result} из стека");
            Paint.SubTitle("\nСтек после функции TryPop:");
            _Stack.Print();
        }
        else
            Paint.Error($"\nНе удалось извлечь элемент из стека");
    }

    // не доделано
    public static void Task()
    {
        Paint.Title("Задачка со стеком");
        Paint.Input("\nВведите строку: ");

        string? s = Console.ReadLine();

        if (s != null)
        {
            char[] input = s.ToCharArray().DeleteLetters();
            Stack<char> stack = new Stack<char>(input);
            Queue<char> queue = new Queue<char>(input);

            string openBrace = "{[(";
            string closeBrace = "}])";

            bool flag = true;
            for (int i = 0; i < input.Length; i++)
            {
                char firstElement = queue.Dequeue();
                char secondElement = stack.Pop();
                if (openBrace.IndexOf(firstElement) != closeBrace.IndexOf(secondElement))
                {
                    Paint.Error("\nНеверные скобки");
                    flag = false;
                    break;
                }
            }
            if (flag) Paint.SubTitle("\nВерные скобки");
        }
        else
        {
            Paint.Error("\nCтрока не введена");
        }
    }
    static char[] DeleteLetters(this char[] array)
    {
        List<char> list = new List<char>(array);
        string openBrace = "{[(";
        string closeBrace = "}])";

        for (int i = 0; i < list.Count; i++)
        {
            if (!openBrace.Contains(list[i]) && !closeBrace.Contains(list[i]))
            {
                list.RemoveAt(list[i]);
                i--;
            } 
        }
        return list.ToArray();
    }

    static void Print(this Stack<char> stack)
    {
        Paint.Write("[ ", ConsoleColor.Red);
        foreach (var item in stack)
        {
            Paint.Write(item + " ", ConsoleColor.Green);
        }
        Paint.WriteLine("]", ConsoleColor.Red);
    }
}
