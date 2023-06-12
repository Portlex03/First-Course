namespace Learning;
using PersonalMenu;
using System;
using System.Collections;

internal static class MySortedList
{
    static SortedList<char, int> _SortedList = new();

    static MySortedList()
    {
        _SortedList.Fill();
    }

    public static string[] text =
    {
        "1: Remove",
        "2: ContainsKey",
        "3: ContainsValue",
        "4: IndexOfKey",
        "5: IndexOfValue",
        "6: RemoveAt",
        "7: TrimExcess",
        "8: Clear",
        "9: TryGetValue",
        "10: Выход в меню"
    };

    delegate void Functions();
    public static void Choising(int number)
    {
        Console.Clear();
        var Functions = new List<Functions>()
        {
            Remove,ContainsKey,ContainsValue,
            IndexOfKey,IndexOfValue,RemoveAt,
            TrimExcess,Clear,TryGetValue
        };
        if (number == Functions.Count) Project.MenuExit(Project.MenulastValue);
        else
        {
            Functions[number]();
            Project.Exit(number);
        }
    }

    static void Fill(this SortedList<char,int> list)
    {
        Random number = new Random();

        int len = number.Next(5, 20);

        for (int i = 0; i < len; i++)
        {
            char key = (char)number.Next(65, 91);
            int value = number.Next(100,1000);
            if (!list.ContainsKey(key)) list.Add(key, value);
            else i--;
        }
    }

    // 1
    static void Remove()
    {
        Paint.Title("Функция Remove");
        Paint.SubTitle("\nИсходный список:");
        Print(_SortedList);

        Paint.Input("\nВведите ключ, который хотите удалить: ");
        char key = char.Parse(Console.ReadLine());

        Paint.SubTitle("\nПолучившийся список:");
        _SortedList.Remove(key);
        Print(_SortedList);
    }

    // 2
    static void ContainsKey()
    {
        Paint.Title("Функция ContainsKey");
        Paint.SubTitle("\nИсходный список:");
        Print(_SortedList);

        Paint.Input("\nВведите ключ, который хотите найти: ");
        char key = char.Parse(Console.ReadLine());

        if (_SortedList.ContainsKey(key))
            Paint.SubTitle($"\nТакой ключ есть, его значение: {_SortedList[key]}");
        else
            Paint.Error("Такого ключа нет в списке");
    }

    // 3
    static void ContainsValue()
    {
        Paint.Title("Функция ContainsValue");
        Paint.SubTitle("\nИсходный список:");
        Print(_SortedList);

        Paint.Input("\nВведите значение, который хотите найти: ");
        int value = int.Parse(Console.ReadLine());

        if (_SortedList.ContainsValue(value))
            Paint.SubTitle($"\nТакое значение есть");
        else
            Paint.Error("\nТакого значения нет в списке");
    }

    // 4
    static void IndexOfKey()
    {
        Paint.Title("Функция IndexOfKey");
        Paint.SubTitle("\nИсходный список:");
        Print(_SortedList);

        Paint.Input("\nВведите ключ: ");
        char key = char.Parse(Console.ReadLine());
        Paint.SubTitle($"\nЕго индекс: {_SortedList.IndexOfKey(key)}");
    }

    // 5
    static void IndexOfValue()
    {
        Paint.Title("Функция IndexOfValue");
        Paint.SubTitle("\nИсходный список:");
        Print(_SortedList);

        Paint.Input("\nВведите значение: ");
        int value = int.Parse(Console.ReadLine());
        Paint.SubTitle($"\nЕго индекс: {_SortedList.IndexOfValue(value)}");
    }

    // 6
    static void RemoveAt()
    {
        Paint.Title("Функция RemoveAt");
        Paint.SubTitle("\nИсходный список:");
        Print(_SortedList);

        Paint.Input("\nВведите индекс удаляемого ключа: ");
        int index = int.Parse(Console.ReadLine());

        Paint.SubTitle("\nПолучившийся список:");
        _SortedList.RemoveAt(index);
        Print(_SortedList);
    }

    // 7 Вставка
    static void TrimExcess()
    {
        Paint.Title("Функция TrimExcess");
        Paint.SubTitle("\nИсходный список:");
        Print(_SortedList);

        Paint.SubTitle($"\nЁмкость списка до функции: {_SortedList.Capacity}");
        _SortedList.TrimExcess();
        Paint.SubTitle($"\nЁмкость списка после функции: {_SortedList.Capacity}");
    }

    // 8
    static void Clear()
    {
        Paint.Title("Функция Clear");
        Paint.SubTitle("\nИсходный список:");
        Print(_SortedList);

        Paint.SubTitle("\nПолучившийся список:");
        _SortedList.Clear();
        Print(_SortedList);
    }

    // 9
    static void TryGetValue()
    {
        Paint.Title("Функция TryGetValue");
        Paint.SubTitle("\nИсходный список:");
        Print(_SortedList);

        Paint.Input("\nВведите ключ: ");
        char key = char.Parse(Console.ReadLine());
        int value;
        if (_SortedList.TryGetValue(key, out value))
            Paint.SubTitle($"\nЗначение ключа: {value}");
        else
            Paint.Error("\nКлюча нет");
    }

    static void Print(this SortedList<char,int> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}
