namespace Learning;
using PersonalMenu;
using System.Collections;
using System.Collections.Generic;

internal static class MyDictionary
{
    readonly static Dictionary<char, int> _Dictionary = new();

    static MyDictionary()
    {
        _Dictionary.Fill();
    }

    delegate void Functions();

    public static string[] text =
    {
        "1: Add",
        "2: ContainsKey",
        "3: ContainsValue",
        "4: Remove",
        "5: TryGetValue",
        "6: Clear",
        "7: Выход в меню"
    };

    public static void Choising(int number)
    {
        Console.Clear();
        var Functions = new List<Functions>()
        {
            Add, ContainsKey, ContainsValue,
            Remove, TryGetValue, Clear
        };
        if (number == Functions.Count) Project.MenuExit(Project.MenulastValue);
        else
        {
            Functions[number]();
            Project.Exit(number);
        }
    }
    static void Fill(this Dictionary<char, int> dictionary)
    {
        Random number = new Random();

        int len = number.Next(5, 20);

        for (int i = 0; i < len; i++)
        {
            char key = (char)number.Next(65, 91);
            int value = number.Next(100, 1000);
            if (!dictionary.ContainsKey(key)) dictionary.Add(key, value);
            else i--;
        }

    }

    //1
    static void Add()
    {
        Paint.Title("Функция Add");
        Paint.SubTitle("\nИсходный словарь:");
        _Dictionary.Print();

        Paint.Input("\nВведите ключ: ");
        char key = char.Parse(Console.ReadLine());
        Paint.Input("\nВведите значение: ");
        int value = int.Parse(Console.ReadLine());
        Paint.SubTitle("\nПолучившийся словарь:");
        _Dictionary.Add(key,value);
        _Dictionary.Print();
    }

    //2
    static void ContainsKey()
    {
        Paint.Title("Функция ContainsKey");
        Paint.SubTitle("\nИсходный словарь:");
        _Dictionary.Print();

        Paint.Input("\nВведите ключ: ");
        char key = char.Parse(Console.ReadLine());

        if (_Dictionary.ContainsKey(key))
            Paint.SubTitle($"\nКлюч есть. Его значение: {_Dictionary[key]}");
        else
            Paint.Error("\nКлюча нет в словаре");
    }

    //3
    static void ContainsValue()
    {
        Paint.Title("Функция ContainsValue");
        Paint.SubTitle("\nИсходный словарь:");
        _Dictionary.Print();

        Paint.Input("\nВведите значение: ");
        int value = int.Parse(Console.ReadLine());

        if (_Dictionary.ContainsValue(value))
            Paint.SubTitle($"\nЗначение есть в словаре.");
        else
            Paint.Error("\nЗначения нет в словаре");
    }

    //4
    static void Remove()
    {
        Paint.Title("Функция Remove");
        Paint.SubTitle("\nИсходный словарь:");
        _Dictionary.Print();

        Paint.Input("\nВведите ключ: ");
        char key = char.Parse(Console.ReadLine());

        Paint.SubTitle("\nПолучившийся словарь:");
        _Dictionary.Remove(key);
        _Dictionary.Print();
    }

    //5
    static void TryGetValue()
    {
        Paint.Title("Функция TryGetValue");
        Paint.SubTitle("\nИсходный словарь:");
        _Dictionary.Print();

        Paint.Input("\nВведите ключ: ");
        char key = char.Parse(Console.ReadLine());
        int value;

        if (_Dictionary.TryGetValue(key, out value))
            Paint.SubTitle($"\nКлюч есть. Его значение: {value}");
        else
            Paint.Error("\nКлюча нет в словаре");
    }

    //6
    static void Clear()
    {
        Paint.Title("Функция Clear");
        Paint.SubTitle("\nИсходный словарь:");
        _Dictionary.Print();

        Paint.SubTitle("\nПолучившийся словарь:");
        _Dictionary.Clear();
        _Dictionary.Print();
    }

    static void Print(this Dictionary<char, int> dictionary)
    {
        foreach (var item in dictionary)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}
