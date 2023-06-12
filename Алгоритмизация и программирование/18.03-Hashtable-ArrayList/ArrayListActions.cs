using PersonalMenu;
using System;
using System.Collections;
using System.Xml.Linq;

namespace Learning;

class ArrayListActions
{
    readonly static ArrayList array = new();

    static ArrayList arrayClone = new();

    static ArrayListActions()
    {
        array = Fill();
    }
    
    public static void SelectAction(int key)
    {
        Console.Clear();
        arrayClone = (ArrayList)array.Clone();
        switch (key)
        {
            case 0: Sort(); break;
            case 1: Clear(); break;
            case 2: AddRange(); break;
            case 3: Reverse(); break;
            case 4: Insert(); break;
            case 5: Contains(); break;
            case 6: RemoveAt(); break;
            case 7: GetRange(); break;
            case 8: TrimToSize(); break;
            case 9: BinarySearch(); break;
            case 10: Project.MenuExit(); break;
        }
        Project.Exit();
    }

    public static string[] menuText = new string[]
    {
        "1: Сортировка",
        "2: Удаление всех элементов",
        "3: Добавление коллекции к массиву",
        "4: Переворачивание массива",
        "5: Вставка значения",
        "6: Проверка на содержание элемента в массиве",
        "7: Удаление элемента",
        "8: Копирование некоторых элементов",
        "9: Установка размера массива",
        "10: Бинарный поиск элемента",
        "11: Выход в меню"
    };

    // заполнение ArrayList
    static ArrayList Fill()
    {
        ArrayList array = new();

        Random number = new Random();
        int n = number.Next(10,20);

        for (int i = 0; i < n; i++)
        {
            array.Add((char)number.Next(65,90));
        }
        return array;
    }

    // вывод массива
    static void Print(ArrayList array)
    {
        Console.Write("[ ");
        foreach (var item in array)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine("] ");
    }

    // 1. Сортировка
    static void Sort()
    {
        Paint.WriteLine("Сортировка arraylist: ",ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ",ConsoleColor.Cyan); Print(array);

        Paint.Write("\nСортированный массив: ", ConsoleColor.Cyan); 
        arrayClone.Sort();
        Print(arrayClone);
    }

    // 2. Удаление всех элементов
    static void Clear()
    {
        Paint.WriteLine("Удаление всех элементов arraylist: ",ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ",ConsoleColor.Cyan); Print(array);

        Paint.Write("\nУдалённый массив: ", ConsoleColor.Cyan);
        arrayClone.Clear();
        Print(arrayClone);
    }

    // 3. Добавление коллекции к массиву 
    static void AddRange()
    {
        Paint.WriteLine("Добавление коллекции к массиву arraylist: ", ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ",ConsoleColor.Cyan); Print(array);

        ArrayList collection = Fill();
        Paint.Write("\nДругая коллекция: ",ConsoleColor.Blue); Print(collection);

        Paint.Write("\nНовый массив: ",ConsoleColor.Cyan);
        arrayClone.AddRange(collection);
        Print(arrayClone);
    }

    // 4. Переворачивание массива
    static void Reverse()
    {
        Paint.WriteLine("Переворачивание массива arraylist: ",ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ",ConsoleColor.Cyan); Print(array);

        Paint.Write("\nПеревёрнутый массив: ",ConsoleColor.Cyan);
        arrayClone.Reverse();
        Print(arrayClone);
    }

    // 5. Вставка значения
    static void Insert()
    {
        Paint.WriteLine("Вставка значения элементу arraylist",ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ",ConsoleColor.Cyan); Print(array);

        Paint.Write("\nВведите индекс, в который хотите вставить значение: ", ConsoleColor.Blue);
        string? strIndex = Console.ReadLine();

        Paint.Write("\nВведите значение элемента: ", ConsoleColor.Blue);
        string? value = Console.ReadLine();

        if (!CheckConvert(strIndex) || value is null) Paint.WriteLine("\nОшибка ввода индекса или значения",ConsoleColor.Red);
        else
        {
            Paint.Write("\nНовый массив: ", ConsoleColor.Cyan);
            arrayClone.Insert(int.Parse(strIndex), value);
            Print(arrayClone);
        }
    }

    // 6. Проверка на содержание элемента в массиве
    static void Contains()
    {
        Paint.WriteLine("Проверка на содержание элемента в arraylist",ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ", ConsoleColor.Cyan); Print(array);

        Paint.Write("\nВведите значение элемента: ",ConsoleColor.Blue);
        string? strValue = Console.ReadLine();
        char value = '0';
        if (strValue != null) value = strValue[0];

        if (arrayClone.Contains(value)) Paint.WriteLine($"\nЭлемент есть. Его индекс {arrayClone.IndexOf(value)}",ConsoleColor.Cyan);
        else Paint.WriteLine($"\nЭлемент {value} отсутствует в arraylist",ConsoleColor.Red);
    }

    // 7. Удаление элемента
    static void RemoveAt()
    {
        Paint.WriteLine("Удаление элемента из arraylist",ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ",ConsoleColor.Cyan); Print(array);

        Paint.Write("\nВведите индекс удаляемого элемента: ", ConsoleColor.Blue);
        string? index = Console.ReadLine();

        if (!CheckConvert(index)) Paint.WriteLine("\nОшибка ввода индекса", ConsoleColor.Red);
        else
        {
            arrayClone.RemoveAt(int.Parse(index));
            Paint.Write("\nНовый массив: ", ConsoleColor.Cyan); Print(arrayClone);
        }
        
    }

    // 8. Копирование некоторых элементов
    static void GetRange()
    {
        Paint.WriteLine("Копирование некоторых элементов arraylist", ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ", ConsoleColor.Cyan); Print(array);

        Paint.Write("\nВведите индекс, с которого будут копироваться значения: ", ConsoleColor.Blue);
        string? index = Console.ReadLine();

        Paint.Write("\nВведите количество копируемых элементов: ", ConsoleColor.Blue);
        string? value = Console.ReadLine();

        if (!CheckConvert(index) || !CheckConvert(value)) Paint.WriteLine("\nОшибка ввода индекса или значения", ConsoleColor.Red);

        Paint.Write("\nНовый массив: ", ConsoleColor.Cyan);
        arrayClone = array.GetRange(int.Parse(index), int.Parse(value));
        Print(arrayClone);
    }

    // 9. Установка размера массива
    static void TrimToSize()
    {
        Paint.WriteLine("Установка размера массива arraylist", ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ", ConsoleColor.Cyan); Print(array);

        Paint.WriteLine($"\nРазмер до функции TrimToSize: {array.Capacity}",ConsoleColor.Cyan);

        arrayClone.TrimToSize();
        Paint.WriteLine($"\nРазмер после функции TrimToSize: {arrayClone.Capacity}",ConsoleColor.Cyan);
    }

    // 10. Бинарный поиск
    static void BinarySearch()
    {
        Paint.WriteLine("Бинарный поиск элемента в arraylist",ConsoleColor.Yellow);
        Paint.Write("\nИсходный массив: ",ConsoleColor.Cyan); Print(array);

        Paint.Write("\nВведите элемент, который хотите найти: ", ConsoleColor.Blue);
        string? strElement = Console.ReadLine();
        char element = '0';

        if (strElement != null) element = strElement[0];

        Paint.Write("\nСортированный массив: ", ConsoleColor.Blue);
        arrayClone.Sort();
        Print(arrayClone);

        int index = arrayClone.BinarySearch(element);
        if (index < 0) Paint.WriteLine("Нет элемента в arraylist",ConsoleColor.Red);
        else Paint.WriteLine($"\nЭлемент {element} находится на {index + 1} месте в сортированном массиве",ConsoleColor.Cyan);
    }

    // Проверка на конвертацию из строки в число
    public static bool CheckConvert(string? number)
    {
        try
        {
            int.Parse(number);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}