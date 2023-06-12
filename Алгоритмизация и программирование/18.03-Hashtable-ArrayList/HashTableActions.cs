using PersonalMenu;
using System.Collections;

namespace Learning;

class HashTableActions
{
    public static string[] menutext = new string[]
    {
        "1: Добавление элемента в таблицу",
        "2: Удаление таблицы",
        "3: Удаление элемента таблицы",
        "4: Нахождение элемента таблицы",
        "5: Нахождение значения элемента таблицы",
        "6: Синхронизация таблицы",
        "7: Хэш код таблицы",
        "8: Копирование таблицы в array",
        "9: Клонирование таблицы",
        "10: Выход в меню"
    };
    public static void SelectAction(int number)
    {
        Console.Clear();
        hashtable = (Hashtable)trueHashtable.Clone();
        switch (number)
        {
            case 0: Add(); break;
            case 1: Clear(); break;
            case 2: Remove(); break;
            case 3: ContainsKey(); break;
            case 4: ContainsValue(); break;
            case 5: Synchronized(); break;
            case 6: GetHashCode(); break;
            case 7: Clone(); break;
            case 8: Project.MenuExit(); break;
        }
        Project.Exit();
    }

    static readonly Hashtable trueHashtable = new();

    static Hashtable hashtable = new();

    static HashTableActions()
    {
        trueHashtable = Fill();
    }

    static Hashtable Fill()
    {
        Random number = new Random();

        var hashtable = new Hashtable();

        var length = number.Next(5, 10);

        for (int i = 0; i < length; i++)
        {
            char key = (char)number.Next(65, 90);
            int value = number.Next(100, 1000);

            if (!hashtable.ContainsKey(key))
            {
                hashtable[key] = value;
            }
            else i--;
        }
        return hashtable;
    }

    // 1.  hashtable.Add
    static void Add()
    {
        Paint.WriteLine("Добавление элемента в таблицу",ConsoleColor.Yellow);
        Paint.WriteLine("\nИсходная таблица:",ConsoleColor.Cyan); Print(trueHashtable);

        Paint.Write("Введите ключ: ", ConsoleColor.Blue);
        string? key = Console.ReadLine();

        Paint.Write("Введите его значение: ", ConsoleColor.Blue);
        string? value = Console.ReadLine();

        if (key != null && value != null)
        {
            hashtable.Add(key, value);
            Paint.WriteLine("\nНовая таблица: ", ConsoleColor.Cyan);
            Print(hashtable);
        }
        else
        {
            Paint.WriteLine("\nОшибка ввода ключа или значения", ConsoleColor.Red);
        }
    }

    // 2.  hashtable.Clear();
    static void Clear()
    {
        Paint.WriteLine("Удаление таблицы", ConsoleColor.Yellow);
        Paint.WriteLine("\nИсходная таблица:", ConsoleColor.Cyan); Print(trueHashtable);

        hashtable.Clear();
        Paint.WriteLine("Удалённая таблица:", ConsoleColor.Cyan);
        Print(hashtable);
    }

    // 3. hashtable.Remove("a");
    static void Remove()
    {
        Paint.WriteLine("Удаление элемента таблицы", ConsoleColor.Yellow);
        Paint.WriteLine("\nИсходная таблица:", ConsoleColor.Cyan); Print(trueHashtable);

        Paint.Write("Введите ключ, который хотите удалить: ", ConsoleColor.Blue);
        string? key = Console.ReadLine();

        if (key is null || !hashtable.Contains(key[0])) Paint.WriteLine("Такого ключа нет в таблице", ConsoleColor.Red);
        else
        {
            hashtable.Remove(key[0]);
            Paint.WriteLine("Новая таблица:", ConsoleColor.Cyan);
            Print(hashtable);
        }
    }

    // 4. hashtable.ContainsKey()
    static void ContainsKey()
    {
        Paint.WriteLine("Нахождение элемента таблицы",ConsoleColor.Yellow);
        Paint.WriteLine("\nИсходная таблица:",ConsoleColor.Cyan); Print(trueHashtable);

        Paint.Write("Введите ключ, который хотите найти: ",ConsoleColor.Blue);
        string? key = Console.ReadLine();

        if (key is null) Paint.WriteLine("Ошибка ввода", ConsoleColor.Red);
        else if (!hashtable.ContainsKey((int)key[0])) Paint.WriteLine($"Ключ есть, его значение: {hashtable[key[0]]}", ConsoleColor.Cyan);
        else Paint.WriteLine("Ключа не найдено", ConsoleColor.Red);
    }

    // 5. hashtable.ContainsValue()
    static void ContainsValue()
    {
        Paint.WriteLine("Нахождение значения элемента таблицы", ConsoleColor.Yellow);
        Paint.WriteLine("\nИсходная таблица:",ConsoleColor.Cyan); Print(trueHashtable);

        Paint.Write("Введите значение ключа, которое хотите найти: ",ConsoleColor.Blue);
        string? value = Console.ReadLine();
        int intValue;

        if (value is null) Paint.WriteLine("Ошибка ввода", ConsoleColor.Red);
        else if (int.TryParse(value,out intValue) && hashtable.ContainsValue(intValue))
        {
            foreach (var key in hashtable.Keys)
            {
                if (hashtable[key] != null && (int)hashtable[key] == intValue)
                {
                    Paint.WriteLine($"Значение найдено, его ключ: {key}",ConsoleColor.Cyan);
                    break;
                }
            }
        }
        else Paint.WriteLine("Ключ не найден", ConsoleColor.Red);
    }

    // 6. Hashtable.Synchronized;
    static void Synchronized()
    {
        Paint.WriteLine("Синхронизация таблицы",ConsoleColor.Yellow);
        Paint.WriteLine("\nИсходная таблица: ",ConsoleColor.Cyan); Print(hashtable);
        if (trueHashtable.IsSynchronized) Paint.WriteLine("Тип таблицы: Синхронизирована",ConsoleColor.Cyan);
        else Paint.WriteLine("Тип таблицы: Не синхронизирована",ConsoleColor.Cyan);

        hashtable = Hashtable.Synchronized(hashtable);

        if (hashtable.IsSynchronized) Paint.WriteLine("Тип таблицы после синхронизации: Синхронизирована", ConsoleColor.Cyan);
        else Paint.WriteLine("Тип таблицы после синхронизации: Не синхронизирована", ConsoleColor.Cyan);
    }

    // 7. hashtable.GetHashCode
    static void GetHashCode()
    {
        Paint.WriteLine("Хэш код таблицы", ConsoleColor.Yellow);
        Paint.WriteLine($"\nХэш код оригинальной таблицы: {trueHashtable.GetHashCode()}", ConsoleColor.Cyan);
        Paint.WriteLine($"\nХэш код копии таблицы: {hashtable.GetHashCode()}", ConsoleColor.Cyan);
    }

    // 8. hashtable.Clone()
    static void Clone()
    {
        Paint.WriteLine("Клонирование таблицы", ConsoleColor.Yellow);
        Paint.WriteLine("\nИсходная таблица:", ConsoleColor.Cyan); Print(trueHashtable);

        Hashtable hashtableClone = (Hashtable)trueHashtable.Clone();
        Paint.WriteLine("Клон таблицы:", ConsoleColor.Cyan);
        Print(hashtableClone);
    }

    // вывод таблицы
    static void Print(Hashtable hashtable)
    {
        foreach (var key in hashtable.Keys)
        {
            Console.WriteLine($"{key}: {hashtable[key]}");
        }
        Console.WriteLine();
    }
}