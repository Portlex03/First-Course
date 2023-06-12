namespace MySpace;
internal class Project
{
    internal static string[] menuText = new string[] { "\t\tМеню", "1 : Заполнение информации о студентах", "2 : Выдать студентов, которые учатся в заданной группе", 
        "3 : Выдать студентов должников", "4 : Выдать студентов отличнков","5 : Выдать студентов моложе 20 лет","6 : Выход"}; // текст меню
    internal static void Menu(List<StudentInfo> Students)
    {
        Console.SetWindowSize(60, 20);
        for (int i = 0; i < menuText.Length; i++)
        {
            string? item = menuText[i];
            Console.WriteLine(item);
        }
        int result = Keys(menuText);
        switch (result)
        {
            case 1: StudentInfo.RegisterStudents(ref Students); break;
            case 2: Task1.GroupFinder(in Students); break;
            case 3: Task2.Debtors(in Students); break;
            case 4: Task3.HonorStudents(in Students); break;
            case 5: Task4.YoungStudents(Students); break;
            case 6: break;
        }
    }
    public static int Keys(string[] menuText)
    {
        int num = 0;
        bool flag = false;
        do
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow: { num--; if (num != -1) PrintText(num,menuText); }; break;
                case ConsoleKey.DownArrow: { num++; if (num != menuText.Length) PrintText(num,menuText); }; break;
                case ConsoleKey.Enter: { flag = true; }; break;
            }

            if (num == menuText.Length)
            {
                num = 1;
                PrintText(num,menuText);
            }
            if (num == 0 || num == -1)
            {
                num = menuText.Length - 1;
                PrintText(num,menuText);
            }

        } while (!flag);
        return num;
    }

    //Вывод текста
    static void PrintText(int button, string[] menuText)
    {
        Console.Clear();
        foreach (string item in menuText)
        {
            if (item == menuText[button] && button != 0) Color(item);
            else Console.WriteLine(item);
        }
    }
    //Покраска текста
    static void Color(String text)
    {
        //Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
