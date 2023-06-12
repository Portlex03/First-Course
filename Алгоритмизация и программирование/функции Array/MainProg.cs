namespace ArrayFunctions;

class MainProg
{
    public static int[] array = new int[10];

    public static string[] menuText = new string[]
    {
            "\t\tМеню",
            "1: Получение индекса элемента массива",
            "2: Переворот массива",
            "3: Изменение размера массива",
            "4: Сортировка массива",
            "5: Бинарный поиск элемента",
            "6: Очистка содержимого массива",
            "7: Заполнение массива одним числом",
            "8: Поиск всех элементов массива, которые больше заданного числа",
            "9: Копирование массива",
            "10: Возведение чисел в квадрат",
            "11: Выход"
    };

    static void Main()
    {
        Fill(ref array);

        // вызов меню
        Project.Menu(menuText, ArrayActions.SelectOperation);
     }
    // заполнение массива
    public static void Fill(ref int[] array)
    {
        Random number = new();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = number.Next(10, 100);
        }
    }
}