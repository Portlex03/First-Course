namespace ArrayFunctions;

internal class ArrayActions
{
    static int[] array = new int[MainProg.array.Length];
    //пырики-пупырики
    public static void SelectOperation(int Chousing)
    {
        switch (Chousing)
        {
            case 1: Console.Clear(); IndexOf();break;
            case 2: Console.Clear(); Reverse();break;
            case 3: Console.Clear(); Resize(); break;
            case 4: Console.Clear(); Sort(); break;
            case 5: Console.Clear(); BinarySearch(); break;
            case 6: Console.Clear(); Clear(); break;
            case 7: Console.Clear(); Fill(); break;
            case 8: Console.Clear(); FindAll(); break;
            case 9: Console.Clear(); Copy(); break;
            case 10: Console.Clear(); Foreach(); break;
            default: Console.Clear(); break;
        }
    }
    // 1) Получение индекса элемента
    public static void IndexOf()
    {
        Project.Color("Получение индекса элемента массива: ");

        Project.Color("\nИсходный массив: ",false);
        Print(MainProg.array);

        Project.Color("\nВведите элемент, индекс которого хотите получить: ",false);
        string? input = Console.ReadLine();
        int index = -1;

        if (Converting(input)) index = Array.IndexOf(MainProg.array, int.Parse(input));

        if (index == -1) Console.WriteLine("Нет элемента в массиве");
        else Console.WriteLine($"Индекс элемента {input} = {index}");

        Project.Exit();
    }

    // 2) переворот массива
    public static void Reverse()
    {
        Project.Color("Переворот массива:");

        Project.Color("Исходный массив: ", false);
        Array.Copy(MainProg.array, array, MainProg.array.Length);
        Print(array);

        Project.Color("\nВведите индекс, с которого будет переворачиваться массив: ", false);
        string? index = Console.ReadLine();

        Project.Color("\nВведите количество переворачивающихся элементов: ", false);
        string? length = Console.ReadLine();

        if (Converting(index) && Converting(length))
        {
            Array.Reverse(array, int.Parse(index), int.Parse(length));
            Project.Color("Перевёрнутый массив: ", false);
            Print(array);
        }
        else
        {
            Project.Color("\nОшибка ввода\nПеревёрнутый массив: ", false);
            Array.Reverse(array);
            Print(array);
        }
        Project.Exit();
    }

    // 3) изменение размера массива
    public static void Resize()
    {
        Project.Color("Изменение размера массива:");

        Project.Color("Исходный массив: ", false);
        Array.Copy(MainProg.array, array, MainProg.array.Length);
        Print(array);

        Project.Color("\nВведите новый размер массива: ", false);
        string? newSize = Console.ReadLine();

        try
        {
            Array.Resize(ref array, int.Parse(newSize));
            Project.Color("\nНовый массив: ", false);
            Print(array);
        }
        catch (Exception)
        {
            Project.Color("\nОшибка: ",false);
            Console.WriteLine("Не удалось изменить размер массива.");
        }
        Project.Exit();
    }

    // 4) сортировка массива 
    public static void Sort()
    {
        Project.Color("Сортировка массива:");

        Project.Color("\nИсходный массив: ", false);
        Array.Copy(MainProg.array, array, MainProg.array.Length);
        Print(array);

        Array.Sort(array);
        Project.Color("Сортированный массив: ", false);
        Print(array);

        Project.Exit();
    }

    // 5) Бинарный поиск элемента
    public static void BinarySearch()
    {
        Project.Color("Бинарный поиск элемента");

        Project.Color("\nИсходный массив: ", false);
        Array.Copy(MainProg.array, array, MainProg.array.Length);
        Print(array);

        Project.Color("Введите элемент, который хотите найти: ", false);
        string? element = Console.ReadLine();

        if (Converting(element))
        {
            Array.Sort(array);
            int index = Array.BinarySearch(array, int.Parse(element));
            if (index != -1) Console.WriteLine($"Индекс элемента {element} в сортированном массиве = {index}");
            else Console.WriteLine("Элемента нет в массиве");
        }
        else
        {
            Project.Color("\nОшибка: ", false);
            Console.WriteLine("Неправильный ввод");
        }
        Project.Exit();
    }

    // 6) Очиcтка содержимого массива
    public static void Clear()
    {
        Project.Color("Очистка содержимого массива:");

        Project.Color("\nИсходный массив: ", false);
        Array.Copy(MainProg.array, array, MainProg.array.Length);
        Print(array);

        Project.Color("\nВведите индекс, с которого будут удаляться элементы массива: ", false);
        string? index = Console.ReadLine();

        Project.Color("\nВведите количество удаляемых элементов: ", false);
        string? length = Console.ReadLine();

        if (Converting(index) && Converting(length))
        {
            Array.Clear(array, int.Parse(index), int.Parse(length));
            Project.Color("Новый массив: ", false);
            Print(array);
        }
        else
        {
            Project.Color("\nОшибка ввода индекса или количетсва\nПустой массив: ", false);
            Array.Clear(array);
            Print(array);
        }
        Project.Exit();
    }

    // 7) Заполнение массива одним числом
    public static void Fill()
    {
        Project.Color("Заполнение массива одним числом:");

        Project.Color("\nИсходный массив: ", false);
        Array.Copy(MainProg.array, array, MainProg.array.Length);
        Print(array);

        Project.Color("Введите элемент, который будет в массиве: ", false);
        string? element = Console.ReadLine();

        Project.Color("\nВведите индекс, с которого будут заменяться элементы массива: ", false);
        string? index = Console.ReadLine();

        Project.Color("\nВведите количество заменяемых элементов: ", false);
        string? length = Console.ReadLine();

        if (Converting(index) && Converting(length) && Converting(element))
        {
            Array.Fill(array, int.Parse(element), int.Parse(index), int.Parse(length));
            Project.Color("Новый массив: ", false);
            Print(array);
        }
        else
        {
            Project.Color("\nОшибка ввода индекса или количества");
            if (Converting(element)) Array.Fill(array,int.Parse(element));
            Project.Color("Новый массив: ", false);
            Print(array);
        }
        Project.Exit();
    }

    // 8) Поиск всех элементов массива, которые больше заданного числа 
    public static void FindAll()
    {
        Project.Color("Поиск всех элементов массива, которые больше заданного числа:");

        Project.Color("\nИсходный массив: ", false);
        Array.Copy(MainProg.array, array, MainProg.array.Length);
        Print(array);

        Project.Color("Введите элемент: ", false);
        string? element = Console.ReadLine();

        if (Converting(element))
        {
            int[] finding = Array.FindAll(array, x => x > int.Parse(element));
            if (finding.Length == 0) Console.WriteLine("Нет элементов, которые больше заданного");
            else 
            {
                Project.Color("Элементы, которые больше заданного: ", false);
                Print(finding);
            }
        }
        else
        {
            Project.Color("\nОшибка ввода элемента");
        }
        Project.Exit();
    }

    // 9) Копирование массива
    public static void Copy()
    {
        Project.Color("Копирование массива");

        Project.Color("\nИсходный массив: ", false);
        Array.Copy(MainProg.array, array, MainProg.array.Length);
        Print(array);

        Project.Color("\nВведите количество копирующихся элементов массива: ", false);
        string? length = Console.ReadLine();

        if (Converting(length))
        {
            int[] array2 = new int[int.Parse(length)];
            Array.Copy(array, array2, int.Parse(length));
            Project.Color("Новый массив: ", false);
            Print(array2);
        }
        else
        {
            Project.Color("\nОшибка ввода индекса");
        }
        Project.Exit();
    }

    // 10) Возведение чисел в квадрат 
    public static void Foreach()
    {
        Project.Color("Возведение чисел в квадрат");

        Project.Color("\nИсходный массив: ", false);
        Array.Copy(MainProg.array, array, MainProg.array.Length);
        Print(array);

        Console.Write("Новый массив: [ ");
        Array.ForEach(array, x => Console.Write((x * x).ToString() + " "));
        Console.WriteLine(" ]");

        Project.Exit();
    }

    // попытка конвертировать число
    public static bool Converting(string? number)
    {
        try
        {
            int.Parse(number);
            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }

    // вывод массива
    public static void Print(int[] array)
    {
        Console.Write("[ ");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine("]\n");
    }
}