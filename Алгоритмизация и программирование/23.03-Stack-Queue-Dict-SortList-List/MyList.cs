namespace Learning;
using PersonalMenu;
using System;
using System.Linq.Expressions;

internal static class MyList 
{
    static List<char> _List = new();

    delegate void Functions();

    static MyList()
    {
        _List.Fill();
    }

    public static string[] text =
    {
        "1: AddRange",
        "2: RemoveRange",
        "3: Contains",
        "4: ConvertAll",
        "5: FindAll",
        "6: IndexOf",
        "7: Insert",
        "8: Remove",
        "9: Reverse",
        "10: Sort",
        "11: Выход в меню"
    };

    public static void Choising(int number)
    {
        Console.Clear();
        List<Functions> Functions = new List<Functions>()
        {
            AddRange,RemoveRange,Contains,
            ConvertAll,FindAll,IndexOf,
            Insert,Remove,Reverse,Sort,
        };
        if (number == Functions.Count) Project.MenuExit(Project.MenulastValue);
        else
        {
            Functions[number]();
            Project.Exit(number);
        }

    }
    static void Fill(this List<char> list)
    {
        Random number = new Random();

        int len = number.Next(5,20);

        for (int i = 0; i < len; i++)
        {
            list.Add((char)number.Next(65, 91));
        }
    }

    // 1
    static void AddRange()
    {
        Paint.Title("Функция AddRange\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.Write("Другая коллекция: ", ConsoleColor.Cyan);
        List<char> _List2 = new();
        _List2.Fill();
        Print(_List2);

        Paint.SubTitle2("Получившися список: ");
        _List.AddRange(_List2);
        Print(_List);
    }

    // 2
    static void RemoveRange()
    {
        Paint.Title("Функция RemoveRange\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.Input("\nВведите индекс, с которого нужно удалять элементы: ");
        int index = int.Parse(Console.ReadLine());
        Paint.Input("Введите количество удаляемых элементов: ");
        int count = int.Parse(Console.ReadLine());
        Paint.SubTitle2("\nПолучившийся список: ");
        _List.RemoveRange(index, count);
        Print(_List);
    }

    // 3
    static void Contains()
    {
        Paint.Title("Функция Contains\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.Input("\nВведите элемент: ");
        char element = char.Parse(Console.ReadLine());

        if (_List.Contains(element)) Paint.SubTitle("\nЭлемент есть в листе");
        else Paint.Error("\nЭлемента нет в листе");
    }

    // 4
    static void ConvertAll()
    {
        Paint.Title("Функция ConvertAll\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.SubTitle2("Лист с заменёнными элементами: ");
        List<int> numbers = _List.ConvertAll(converter => (int)converter);
        Print(numbers);
    }

    // 5
    static void FindAll()
    {
        Paint.Title("Функция FindAll\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.SubTitle2("Элементы, которые позже буквы *G*: ");
        List<char> answer = _List.FindAll(x => x > 71);
        Print(answer);
    }

    // 6
    static void IndexOf()
    {
        Paint.Title("Функция IndexOf\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.Input("Введите букву, индекс которой хотите найти: ");
        char element = char.Parse(Console.ReadLine());
        Paint.SubTitle($"Индекс элемента {element}: {_List.IndexOf(element)}");
    }

    // 7 Вставка
    static void Insert()
    {
        Paint.Title("Функция Insert\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.Input("\nВведите индекс, куда хотите вставить элемент: ");
        int index = int.Parse(Console.ReadLine());
        Paint.Input("Введите букву: ");
        char item = char.Parse(Console.ReadLine());
        Paint.SubTitle2("\nПолучившийся список: ");
        _List.Insert(index, item);
        Print(_List);
    }

    // 8
    static void Remove()
    {
        Paint.Title("Функция Remove\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.Input("Введите букву, которую хотите удалить: ");
        char item = char.Parse(Console.ReadLine());
        Paint.SubTitle2("\nПолучившийся список: ");
        _List.Remove(item);
        Print(_List);
    }

    // 9
    static void Reverse()
    {
        Paint.Title("Функция Reverse\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.Input("\nВведите индекс, с которого нужно переворачивать элементы: ");
        int index = int.Parse(Console.ReadLine());
        Paint.Input("Введите количество переворачиваемых элементов: ");
        int count = int.Parse(Console.ReadLine());
        Paint.SubTitle2("\nПолучившийся список: ");
        _List.Reverse(index, count);
        Print(_List);
    }

    // 10
    static void Sort()
    {
        Paint.Title("Функция Sort\n");
        Paint.SubTitle2("Исходный лист: ");
        Print(_List);

        Paint.SubTitle2("\nCортированный список: ");
        _List.Sort();
        Print(_List);
    }

    static void Print<T>(List<T> list)
    {
        Paint.Write("[ ", ConsoleColor.Red);
        foreach (var item in list)
        {
            Paint.Write(item + " ", ConsoleColor.Green);
        }
        Paint.WriteLine("]", ConsoleColor.Red);
    }
}
