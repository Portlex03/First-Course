namespace Learning;
using PersonalMenu;
using System.Collections;
using System.Collections.Generic;

internal static class MyQueue
{
    static readonly Queue<char> _Queue = new();

    static MyQueue()
    {
        _Queue.Fill();
    }

    delegate void Functions();

    public static string[] text =
    {
        "1: Contains",
        "2: Dequeue",
        "3: Enqueue",
        "4: Peek",
        "5: TryDequeue",
        "6: TryPeek",
        "7: Выход в меню"
    };

    public static void Choising(int number)
    {
        Console.Clear();
        var Functions = new List<Functions>()
        {
            Contains, Dequeue, Enqueue,
            Peek, TryDequeue, TryPeek
        };
        if (number == Functions.Count) Project.MenuExit(Project.MenulastValue);
        else
        {
            Functions[number]();
            Project.Exit(number);
        }
    }

    static void Fill(this Queue<char> queue)
    {
        Random number = new Random();

        int len = number.Next(5, 20);

        for (int i = 0; i < len; i++)
        {
            char key = (char)number.Next(65, 91);
            queue.Enqueue(key);
        }
    }

    //1
    static void Contains()
    {
        Paint.Title("Функция Contains");
        Paint.SubTitle("\nИсходная коллекция:");
        _Queue.Print();

        Paint.Input("\nВведите элемент: ");
        char element = char.Parse(Console.ReadLine());

        if (_Queue.Contains(element))
            Paint.SubTitle($"\nЭлемент {element} содержится в коллекции");
        else
            Paint.Error("\nЭлемента нет в коллекции");
    }

    //2
    static void Dequeue()
    {
        Paint.Title("Функция Dequeue");
        Paint.SubTitle("\nИсходная коллекция:");
        _Queue.Print();

        char element = _Queue.Dequeue();
        Paint.SubTitle2($"\nПервый элемент очереди: {element}");

        Paint.SubTitle("\nОчередь после функции Dequeue:");
        _Queue.Print();
    }

    //3
    static void Enqueue()
    {
        Paint.Title("Функция Enqueue");
        Paint.SubTitle("\nИсходная коллекция:");
        _Queue.Print();

        Paint.Input("\nВведите элемент: ");
        char element = char.Parse(Console.ReadLine());

        Paint.SubTitle("\nОчередь после добавления элемента:");
        _Queue.Enqueue(element);
        _Queue.Print();
    }

    //4
    static void Peek()
    {
        Paint.Title("Функция Dequeue");
        Paint.SubTitle("\nИсходная коллекция:");
        _Queue.Print();

        char element = _Queue.Peek();
        Paint.SubTitle2($"\nПервый элемент очереди: {element}");

        Paint.SubTitle("\nОчередь после функции Peek:");
        _Queue.Print();
    }

    //5
    static void TryDequeue()
    {
        Paint.Title("Функция TryDequeue");
        Paint.SubTitle("\nИсходная коллекция:");
        _Queue.Print();

        char result;
        if (_Queue.TryDequeue(out result))
        {
            Paint.SubTitle($"\nУдалось извлечь элемент {result} из очереди");
            Paint.SubTitle("\nОчередь после функции TryDequeue:");
            _Queue.Print();
        }
        else
            Paint.Error($"\nНе удалось извлечь элемент из очереди");
    }

    //6
    static void TryPeek()
    {
        Paint.Title("Функция TryDequeue");
        Paint.SubTitle("\nИсходная коллекция:");
        _Queue.Print();

        char result;
        if (_Queue.TryPeek(out result))
        {
            Paint.SubTitle($"\nУдалось извлечь элемент {result} из очереди");
            Paint.SubTitle("\nОчередь после функции TryPeek:");
            _Queue.Print();
        }
        else
            Paint.Error($"\nНе удалось извлечь элемент из очереди");
    }

    static void Print(this Queue<char> queue)
    {
        Paint.Write("[ ", ConsoleColor.Red);
        foreach (var item in queue)
        {
            Paint.Write(item + " ", ConsoleColor.Green);
        }
        Paint.WriteLine("]", ConsoleColor.Red);
    }
}
